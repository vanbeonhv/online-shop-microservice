namespace EventBus.Message;

public record IntegrationBaseEvent: IIntegrationBaseEvent
{
    public DateTime CreationDate { get;} = DateTime.UtcNow;
    public Guid Id { get; set; }
}