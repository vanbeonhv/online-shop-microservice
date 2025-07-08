using Shared.SeedWork;

namespace Shared.DTOs.Inventory;

public class GetInventoryPagingQuery : PagingRequestParameters
{
    public string itemNo { get; set; }

    public string? SearchTerm { get; set; }
}