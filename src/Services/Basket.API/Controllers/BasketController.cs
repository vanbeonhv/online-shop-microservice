using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories.Interfaces;
using EventBus.Message.IntegrationEvents.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly StockItemGrpcService _stockItemGrpcService;
    private readonly IMapper _mapper;

    public BasketController(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint, IMapper mapper, StockItemGrpcService stockItemGrpcService)
    {
        _basketRepository = basketRepository;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
        _stockItemGrpcService = stockItemGrpcService;
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult> GetBasketByUserName(string userName)
    {
        var result = await _basketRepository.GetBasketByUserName(userName);
        if (result == null)
            return NotFound("Basket not found for the user");
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> UpdateBasket([FromBody] Cart cart)
    {
        // Comunicate with the gRPC stock service to check stock availability
        foreach (var cartItem in cart.Items)
        {
            var stock = await _stockItemGrpcService.GetStock(cartItem.ItemNo);
            cartItem.SetAvailableQuantity(stock.AvailableQuantity);
        }
        
        var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromHours(1))
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));
       
        var result = await _basketRepository.UpdateBasket(cart, options);
        return Ok(result);
    }
    
    [HttpDelete("{userName}")]
    public async Task<ActionResult> DeleteBasket(string userName)
    {
        var result = await _basketRepository.DeleteBasket(userName);
        if (!result)
            return NotFound();
        return NoContent();
    }
    
    [HttpPost("checkout")]
    public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
        var basket = await _basketRepository.GetBasketByUserName(basketCheckout.UserName);
        if (basket == null)
            return NotFound("Basket not found for the user.");

        var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
        
        eventMessage.TotalPrice = basket.TotalPrice;
        await _publishEndpoint.Publish(eventMessage);
        // Optionally, you can clear the basket after checkout
        await _basketRepository.DeleteBasket(basketCheckout.UserName);
        
        return Accepted();
    }
}