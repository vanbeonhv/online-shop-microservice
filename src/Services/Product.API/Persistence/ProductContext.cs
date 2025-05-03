using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;

namespace Product.API.Persistence;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
    }

    public DbSet<CatalogProduct> CatalogProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CatalogProduct>().HasIndex(p => p.No).IsUnique();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
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

        return base.SaveChangesAsync(cancellationToken);
    }
}