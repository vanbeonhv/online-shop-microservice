using Microsoft.EntityFrameworkCore;

namespace Inventory.GrpcService.Persistence;

public class InventoryContext : DbContext
{
    public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
    {
    }

    public DbSet<Production.API.Entities.Inventory> Inventory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Production.API.Entities.Inventory>();
        base.OnModelCreating(modelBuilder);
    }
}