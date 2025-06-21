using Contracts.Common.Events;
using Infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Extensions;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext dbContext, ILogger logger,
        List<BaseEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
            var data = new SerializeService().Serialize(domainEvent);
            logger.Information("\n-----\n A domain event has been published!\n" +
                               "Event: {DomainEvenName} -----\n" +
                               "Data: {Data} ", domainEvent.GetType().Name, data);
        }
    }
}