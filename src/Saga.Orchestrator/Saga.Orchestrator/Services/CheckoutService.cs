using AutoMapper;
using Saga.Orchestrator.HttpRepository.Interfaces;
using Saga.Orchestrator.Services.Interfaces;
using Shared.DTOs.Basket;
using Shared.DTOs.Order;
using ILogger = Serilog.ILogger;

namespace Saga.Orchestrator.Services;

public class CheckoutService: ICheckoutService
{
    private readonly IOrderHttpRepository _orderHttpRepository;
    private readonly IBasketHttpRepository _basketHttpRepository;
    private readonly IInventoryHttpRepository _inventoryHttpRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public CheckoutService(IOrderHttpRepository orderHttpRepository, IBasketHttpRepository basketHttpRepository, IInventoryHttpRepository inventoryHttpRepository, IMapper mapper, ILogger logger)
    {
        _orderHttpRepository = orderHttpRepository;
        _basketHttpRepository = basketHttpRepository;
        _inventoryHttpRepository = inventoryHttpRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<bool> CheckoutOrder(string username, BasketCheckoutDto basketCheckout)
    {
        // Get cart from BasketHttpRepository
        _logger.Information("Start: Get Cart {Username}", username);
        var cart = await _basketHttpRepository.GetBasket(username);
        if (cart == null) return false;
        _logger.Information("End: Get Cart {Username} success", username);
        
        // Create Order from Order
        _logger.Information("Start: Create Order");
        var order = _mapper.Map<CreateOrderDto>(basketCheckout);
        order.TotalPrice = cart.TotalPrice;
        var orderId = await _orderHttpRepository.CreateOrder(order);
        if (orderId <= 0) return false;
        _logger.Information("End: Create Order");
        
        // Get Order by OrderId
        
        
        // Sales Items from InventoryHttpRepository
        
        // Rollback checkout order
        return true;
    }
    
    private void RollBackCheckoutOrder(string username, BasketCheckoutDto basketCheckoutDto)
    {
        return;
    }
}