using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Inventory.GrpcService.Persistence;
using Inventory.Production.API.Repositories;
using Inventory.Production.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.GrpcService.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<InventoryContext>(options =>
            options.UseNpgsql(connectionString!));

        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IUnitOfWork<InventoryContext>, UnitOfWork<InventoryContext>>();

        return services;
    }

    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        return services;
    }
}