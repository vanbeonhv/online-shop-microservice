using Contracts.Common.Interfaces;
using Shared.DTOs.Inventory;

namespace Inventory.Production.API.Services.Interfaces;

public interface IInventoryService
{
    Task<IEnumerable<InventoryDto>> GetAllByItemNoAsync(string itemNo);
    Task<IEnumerable<InventoryDto>> GetAllByItemNoPagingAsync(GetInventoryPagingQuery query);
    Task<InventoryDto> GetOneByIdAsync(long id);
    Task<InventoryDto> PurchaseItemAsync(string itemNo, InventoryDto inventoryDto);
    Task<InventoryDto> SalesItemAsync(string itemNo, SalesProductDto model);
    Task<bool> DeleteByDocumentNoAsync(string documentNo);
}