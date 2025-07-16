using Contracts.Common.Interfaces;

namespace Inventory.Production.API.Repositories.Interfaces;

public interface IInventoryRepository : IRepositoryBaseAsync<Entities.Inventory, long>
{
    Task<IEnumerable<Entities.Inventory>> FindAllByItemNoAsync(string itemNo);
    IQueryable<Entities.Inventory> GetBaseQueryByItemNo(string itemNo, string? searchTerm);
    Task<int> GetStockByItemNoAsync(string itemNo);
}