using Shared.Enums;

namespace Shared.DTOs.Inventory;

public class SalesProductDto
{
    public DocumentType DocumentType = DocumentType.Sale;
    public string ItemNo { get; set; }
    public int Quantity { get; set; }
    public string ExternalDocumentNo { get; set; }
}