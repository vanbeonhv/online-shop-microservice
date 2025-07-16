using Microsoft.EntityFrameworkCore;

namespace Inventory.Production.API.Persistence;

public static class InventoryContextSeed
{
    public static async Task<IHost> SeedInventoryData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var inventoryContext = scope.ServiceProvider.GetRequiredService<InventoryContext>();
        await inventoryContext.Database.MigrateAsync();

        await CreateInventory(inventoryContext);
        return host;
    }

    private static async Task CreateInventory(InventoryContext inventoryContext)
    {
        var inventories = await inventoryContext.Inventory.AnyAsync();
        if (!inventories)
        {
            var newInventory = new Entities.Inventory()
            {
                ItemNo = "IP003",
                Quantity = 10
            };
            
            await inventoryContext.Inventory.AddAsync(newInventory);
            await inventoryContext.SaveChangesAsync();
        }
    }
}