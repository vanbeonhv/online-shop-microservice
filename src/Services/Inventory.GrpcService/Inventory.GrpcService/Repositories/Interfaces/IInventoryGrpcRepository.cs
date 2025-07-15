using Contracts.Common.Interfaces;

namespace Inventory.GrpcService.Repositories.Interfaces;

public interface IInventoryGrpcRepository: IRepositoryBaseAsync<Production.API.Entities.Inventory, long>
{
    Task<int> GetStockByItemNoAsync(string itemNo);
    
}