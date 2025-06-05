namespace EventBus.Message;

public interface IIntegrationBaseEvent
{
    DateTime CreationDate { get; }
    Guid Id { get; set; }
}