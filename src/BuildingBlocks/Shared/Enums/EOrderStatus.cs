namespace Shared.Enums;

public enum EOrderStatus
{
    New = 1, //Start with 1, 0 is used for filter All = 0
    Pending = 2,
    Paid = 3,
    Shipping = 4,
    Fulfilled = 5,
}