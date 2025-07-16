using Grpc.Core;
using Inventory.GrpcService.Repositories.Interfaces;
using Inventory.InventoryGrpcService;
using ILogger = Serilog.ILogger;

namespace Inventory.GrpcService.Services;

public class InventoryGrpcService : StockProtoService.StockProtoServiceBase
{
    private readonly IInventoryGrpcRepository _grpcRepository;
    private readonly ILogger _logger;

    public InventoryGrpcService(IInventoryGrpcRepository grpcRepository, ILogger logger)
    {
        _grpcRepository = grpcRepository;
        _logger = logger;
    }

    public override async Task<StockResponse> GetStock(GetStockRequest request, ServerCallContext context)
    {
        try
        {
            _logger.Information("BEGIN Get Stock of ItemNo: {ItemNo}", request.ItemNo);
            var availableQuantity = await _grpcRepository.GetStockByItemNoAsync(request.ItemNo);
            var stockResponse = new StockResponse()
            {
                AvailableQuantity = availableQuantity
            };
            _logger.Information("END Get Stock of ItemNo: {ItemNo} - Quantity: {Quantity}", request.ItemNo,
                availableQuantity);
            return stockResponse;
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error occurred while getting stock for ItemNo: {ItemNo}", request.ItemNo);
            throw;
        }
    }
}