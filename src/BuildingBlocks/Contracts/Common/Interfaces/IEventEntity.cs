using Contracts.Common.Events;
using Contracts.Domains.Interfaces;
using MediatR;

namespace Contracts.Common.Interfaces;

public interface IEventEntity
{
    void AddDomainEvent(BaseEvent domainEvent);
    void RemoveDomainEvent(BaseEvent domainEvent);
    void ClearDomainEvent();
    IReadOnlyCollection<BaseEvent> GetDomainEvents();
}

public interface IEventEntity<T> : IEntityBase<T>, IEventEntity
{
}