using Microsoft.AspNetCore.Mvc;
using Saga.Orchestrator.Services.Interfaces;
using Shared.DTOs.Basket;
using ILogger = Serilog.ILogger;

namespace Saga.Orchestrator.Controllers;

public class CheckoutController
{
    private readonly ICheckoutSageService _checkoutSageService;
    private readonly ILogger _logger;

    public CheckoutController(ICheckoutSageService checkoutSageService, ILogger logger)
    {
        _checkoutSageService = checkoutSageService;
        _logger = logger;
    }

    [HttpPost("{username}")]
    public async Task<IActionResult> CheckoutOrder(string username, [FromBody] BasketCheckoutDto model)
    {
        var result = await _checkoutSageService.CheckoutOrder(username, model);
        _logger.Information("Checkout Order Result: {Result}", result);
        return new OkResult();
    }
}