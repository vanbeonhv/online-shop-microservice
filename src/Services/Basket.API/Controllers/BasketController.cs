using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;

    public BasketController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult> GetBasketByUserName(string userName)
    {
        var result = await _basketRepository.GetBasketByUserName(userName);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> UpdateBasket([FromBody] Cart cart)
    {
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
}