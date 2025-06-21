using System.Reflection;
using Contracts.Common.Events;
using Contracts.Common.Interfaces;
using Contracts.Domains.Interfaces;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Serilog;

namespace Ordering.Infrastructure.Persistence;

public class OrderContext : DbContext
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public OrderContext(DbContextOptions<OrderContext> options, IMediator mediator, ILogger logger) : base(options)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public DbSet<Order> Order { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Đoạn này sẽ apply hết các configure, miễn là kế thừa từ IEntityTypeConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    private List<BaseEvent> GetDomainEvents()
    {
        var domainEntities = ChangeTracker.Entries<IEventEntity>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .ToList();


        var domainEvents = domainEntities.SelectMany(e => e.GetDomainEvents()).ToList();
        domainEntities.ForEach(d => d.ClearDomainEvent());
        return domainEvents;
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = GetDomainEvents();
        var modifiedEntries = ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Modified or EntityState.Added or EntityState.Deleted)
            .ToList();


        foreach (var entry in modifiedEntries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity is IDateTracking addedEntity)
                    {
                        addedEntity.CreatedDate = DateTime.UtcNow;
                    }

                    break;
                case EntityState.Modified:
                    Entry(entry.Entity).Property("Id").IsModified = false;
                    if (entry.Entity is IDateTracking modifiedEntity)
                    {
                        modifiedEntity.LastModifiedDate = DateTime.UtcNow;
                    }

                    break;
            }
        }

        var result = base.SaveChangesAsync(cancellationToken);

        _mediator.DispatchDomainEventsAsync(this, _logger, domainEvents);
        return result;
    }
}