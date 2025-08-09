using Shared.Enums;

namespace Shared.DTOs.Inventory;

public class InventoryDto: SalesProductDto
{
    public long Id { get; set; }
    public string DocumentNo { get; set; }
}