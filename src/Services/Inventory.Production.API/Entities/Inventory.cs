using Contracts.Domains;
using Shared.Enums;

namespace Inventory.Production.API.Entities;

public class Inventory : EntityAuditBase<long>
{
    public DocumentType DocumentType { get; set; } = DocumentType.Purchase;
    public string DocumentNo { get; set; } = Guid.NewGuid().ToString();
    public string ItemNo { get; set; }
    public int Quantity { get; set; }
    public string ExternalDocumentNo { get; set; } = Guid.NewGuid().ToString();
}