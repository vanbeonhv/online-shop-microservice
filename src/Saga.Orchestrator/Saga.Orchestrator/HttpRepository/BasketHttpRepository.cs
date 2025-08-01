using Saga.Orchestrator.HttpRepository.Interfaces;
using Shared.DTOs.Basket;

namespace Saga.Orchestrator.HttpRepository;

public class BasketHttpRepository: IBasketHttpRepository
{
    private readonly HttpClient _httpClient;

    public BasketHttpRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CartDto?> GetBasket(string userName)
    {
        var cart  = await _httpClient.GetFromJsonAsync<CartDto>($"baskets/{userName}");
        if (cart == null || !cart.Items.Any()) return null;
        return cart;
    }

    public async Task<bool> DeleteBasket(string userName)
    {
        throw new NotImplementedException();
    }
}