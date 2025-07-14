using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Inventory.Production.API.Persistence;
using Inventory.Production.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.Inventory;

namespace Inventory.Production.API.Repositories;

public class InventoryRepository : RepositoryBaseAsync<Entities.Inventory, long, InventoryContext>, IInventoryRepository
{
    public InventoryRepository(InventoryContext dbContext, IUnitOfWork<InventoryContext> unitOfWork) : base(dbContext,
        unitOfWork)
    {
    }


    public async Task<IEnumerable<Entities.Inventory>> FindAllByItemNoAsync(string itemNo)
    {
        return await FindByCondition(i => i.ItemNo.Equals(itemNo)).ToListAsync();
    }

    public IQueryable<Entities.Inventory> GetBaseQueryByItemNo(string itemNo, string? searchTerm)
    {
        var filter = FindByCondition(item => item.ItemNo.Equals(itemNo));
        if (!string.IsNullOrEmpty(searchTerm))
        {
            filter = filter.Where(x => x.DocumentNo.Contains(searchTerm));
        }

        return filter;
    }

    public async Task<int> GetStockByItemNoAsync(string itemNo)
    {
        return await FindByCondition(i => i.ItemNo.Equals(itemNo)).Select(i => i.Quantity).SumAsync();
    }
}