using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Inventory.GrpcService.Repositories;
using Inventory.GrpcService.Repositories.Interfaces;
using Inventory.Production.API.Persistence;
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
        
        services.AddScoped<IUnitOfWork<InventoryContext>, UnitOfWork<InventoryContext>>();
        services.AddScoped<IInventoryGrpcRepository, InventoryGrpcRepository>();

        return services;
    }
}