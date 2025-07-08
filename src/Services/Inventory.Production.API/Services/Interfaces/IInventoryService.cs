using Contracts.Common.Interfaces;
using Shared.DTOs.Inventory;
using Shared.SeedWork;

namespace Inventory.Production.API.Services.Interfaces;

public interface IInventoryService : IRepositoryBaseAsync<Entities.Inventory, long>
{
    Task<IEnumerable<InventoryDto>> GetAllByItemNoAsync(string itemNo);
    Task<IEnumerable<InventoryDto>> GetAllByItemNoPagingAsync(GetInventoryPagingQuery query);
    Task<InventoryDto> GetOneByIdAsync(long id);
    Task<InventoryDto> PurchaseItemAsync(string itemNo, InventoryDto inventoryDto);
}