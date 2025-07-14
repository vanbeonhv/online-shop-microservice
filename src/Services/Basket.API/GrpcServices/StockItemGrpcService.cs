using Inventory.InventoryGrpcService;
using ILogger = Serilog.ILogger;

namespace Basket.API.GrpcServices;

public class StockItemGrpcService
{
    private readonly ILogger _logger;
    private readonly StockProtoService.StockProtoServiceClient _stockProtoServiceClient;

    public StockItemGrpcService(ILogger logger, StockProtoService.StockProtoServiceClient stockProtoServiceClient)
    {
        _logger = logger;
        _stockProtoServiceClient = stockProtoServiceClient;
    }


    public async Task<StockResponse> GetStock(string itemNo)
    {
        var request = new GetStockRequest()
        {
            ItemNo = itemNo
        };
        var result = await _stockProtoServiceClient.GetStockAsync(request);
        return result;
    }
}