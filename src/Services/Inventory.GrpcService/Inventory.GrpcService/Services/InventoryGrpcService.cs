using Grpc.Core;
using Inventory.InventoryGrpcService;
using Inventory.Production.API.Repositories.Interfaces;
using ILogger = Serilog.ILogger;

namespace Inventory.GrpcService.Services;

public class InventoryGrpcService : StockProtoService.StockProtoServiceBase
{
    private readonly IInventoryRepository _repository;
    private readonly ILogger _logger;

    public InventoryGrpcService(IInventoryRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public override async Task<StockResponse> GetStock(GetStockRequest request, ServerCallContext context)
    {
        _logger.Information("BEGIN Get Stock of ItemNo: {ItemNo}", request.ItemNo);
        var availableQuantity = await _repository.GetStockByItemNoAsync(request.ItemNo);
        var stockResponse = new StockResponse()
        {
            AvailableQuantity = availableQuantity
        };
        _logger.Information("END Get Stock of ItemNo: {ItemNo} - Quantity: {Quantity}", request.ItemNo, availableQuantity);
        return stockResponse;
    }
}