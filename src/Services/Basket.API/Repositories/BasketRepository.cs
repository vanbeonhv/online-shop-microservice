using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCacheService;
    private readonly ISerializeService _serializeService;
    private readonly ILogger _logger;

    public BasketRepository(IDistributedCache redisCacheService, ISerializeService serializeService, ILogger logger)
    {
        _redisCacheService = redisCacheService;
        _serializeService = serializeService;
        _logger = logger;
    }

    public async Task<Cart> GetBasketByUserName(string userName)
    {
        var cart = await _redisCacheService.GetStringAsync(userName);
        return string.IsNullOrEmpty(cart) ? null : _serializeService.Deserialize<Cart>(cart);
    }

    public async Task<Cart> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
    {
        if (options == null)
            await _redisCacheService.SetStringAsync(cart.UserName, _serializeService.Serialize(cart));
        else
            await _redisCacheService.SetStringAsync(cart.UserName, _serializeService.Serialize(cart), options);
        
        return cart;
    }

    public async Task<bool> DeleteBasket(string userName)
    {
        try
        {
            await _redisCacheService.RemoveAsync(userName);
            return true;
        }
        catch (Exception e)
        {
            _logger.Error("error delete cache basket {Message}", e.Message);
            return false;
        }
    }
}