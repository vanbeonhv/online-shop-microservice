using Microsoft.EntityFrameworkCore;

namespace Inventory.Production.API.Persistence;

public class InventoryContext : DbContext
{
    public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
    {
    }

    public DbSet<Entities.Inventory> Inventory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.Inventory>();
        base.OnModelCreating(modelBuilder);
    }
}