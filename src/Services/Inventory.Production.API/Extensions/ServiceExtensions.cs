using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Inventory.Production.API.Persistence;
using Inventory.Production.API.Repositories;
using Inventory.Production.API.Repositories.Interfaces;
using Inventory.Production.API.Services;
using Inventory.Production.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Production.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<InventoryContext>(options =>
            options.UseNpgsql(connectionString!, npgsqlOptionsAction => npgsqlOptionsAction.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorCodesToAdd: null
            )));

        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IUnitOfWork<InventoryContext>, UnitOfWork<InventoryContext>>();

        return services;
    }

    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IInventoryService, InventoryService>();
        services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
        return services;
    }
}