using Shared.DTOs.Basket;

namespace Saga.Orchestrator.Services.Interfaces;

public interface ICheckoutSageService
{
    Task<bool> CheckoutOrder(string username, BasketCheckoutDto basketCheckout);
}