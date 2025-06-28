using Inventory.Production.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Production.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<InventoryContext>(options =>
            options.UseNpgsql(connectionString!));
        return services;
    }
}