using Microsoft.Extensions.Caching.Distributed;
using Shared.DTOs.Basket;

namespace Basket.API.Repositories.Interfaces;

public interface IBasketRepository
{
    Task<CartDto?> GetBasketByUserName(string userName);
    Task<CartDto> UpdateBasket(CartDto cart, DistributedCacheEntryOptions options = null);
    Task<bool> DeleteBasket(string userName);
}