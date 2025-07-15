using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Inventory.GrpcService.Repositories.Interfaces;
using Inventory.Production.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.GrpcService.Repositories;

public class InventoryGrpcRepository : RepositoryBaseAsync<Production.API.Entities.Inventory, long, InventoryContext>,
    IInventoryGrpcRepository
{
    public InventoryGrpcRepository(InventoryContext dbContext, IUnitOfWork<InventoryContext> unitOfWork) : base(
        dbContext,
        unitOfWork)
    {
    }


    public async Task<int> GetStockByItemNoAsync(string itemNo)
    {
        return await FindByCondition(i => i.ItemNo.Equals(itemNo)).Select(i => i.Quantity).SumAsync();
    }
}