namespace EventBus.Message.IntegrationEvents.Interfaces;

public interface IBasketCheckoutEvent : IIntegrationBaseEvent
{
    string UserName { get; set; }
    decimal TotalPrice { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string EmailAddress { get; set; }
    string ShippingAddress { get; set; }
    string InvoiceAddress { get; set; }
}