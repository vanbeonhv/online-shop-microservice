using Shared.Enums;

namespace Shared.DTOs.Inventory;

public class InventoryDto
{
    public long Id { get; set; }
    public DocumentType DocumentType { get; set; }
    public string DocumentNo { get; set; }
    public string ItemNo { get; set; }
    public int Quantity { get; set; }
    public string ExternalDocumentNo { get; set; }
}