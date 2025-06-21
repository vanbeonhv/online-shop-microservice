using Contracts.Common.Events;

namespace Ordering.Domain.OrderAggregate.Events;

public class OrderCreatedEvent : BaseEvent
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public decimal TotalPrice { get; set; }
    public string EmailAddress { get; set; }
    public string ShippingAddress { get; set; }
    public string InvoiceAddress { get; set; }

    public OrderCreatedEvent(long id, string userName, decimal totalPrice, string emailAddress, string shippingAddress,
        string invoiceAddress)
    {
        Id = id;
        UserName = userName;
        TotalPrice = totalPrice;
        EmailAddress = emailAddress;
        ShippingAddress = shippingAddress;
        InvoiceAddress = invoiceAddress;
    }
}