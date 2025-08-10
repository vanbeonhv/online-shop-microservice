using AutoMapper;
using Saga.Orchestrator.HttpRepository.Interfaces;
using Saga.Orchestrator.Services.Interfaces;
using Shared.DTOs.Basket;
using Shared.DTOs.Inventory;
using Shared.DTOs.Order;
using ILogger = Serilog.ILogger;

namespace Saga.Orchestrator.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IOrderHttpRepository _orderHttpRepository;
    private readonly IBasketHttpRepository _basketHttpRepository;
    private readonly IInventoryHttpRepository _inventoryHttpRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public CheckoutService(IOrderHttpRepository orderHttpRepository, IBasketHttpRepository basketHttpRepository,
        IInventoryHttpRepository inventoryHttpRepository, IMapper mapper, ILogger logger)
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
        var addedOrder = await _orderHttpRepository.GetOrder(orderId);
        _logger.Information("End: Create Order success, Order Id: {OrderId} - Document No - {DocumentNo}", orderId,
            addedOrder.DocumentNo);


        var inventoryDocumentNos = new List<string>();
        try
        {
            // Sales Items from InventoryHttpRepository
            foreach (var item in cart.Items)
            {
                _logger.Information($"Start: Sale Item No: {item.ItemNo} - Quantity: {item.Quantity}");

                var saleOrder = new SalesProductDto
                {
                    ExternalDocumentNo = addedOrder.DocumentNo,
                    Quantity = item.Quantity,
                    ItemNo = item.ItemNo
                };
                var documentNo = await _inventoryHttpRepository.CreateSalesOrder(saleOrder);
                inventoryDocumentNos.Add(documentNo);

                _logger.Information(
                    $"End: Sale Item No: {item.ItemNo} - Quantity: {item.Quantity} - Document No: {documentNo}");
            }
        }
        catch (Exception e)
        {
            _logger.Error("{Message}", e.Message);
            RollBackCheckoutOrder(username, orderId, inventoryDocumentNos);
            return false;
        }


        // Rollback checkout order
        return true;
    }

    private void RollBackCheckoutOrder(string username, long orderId, List<string> inventoryDocumentNos)
    {
        return;
    }
}