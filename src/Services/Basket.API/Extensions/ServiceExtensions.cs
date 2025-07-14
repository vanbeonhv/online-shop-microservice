using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Inventory.InventoryGrpcService;
using MassTransit;
using Shared.Configurations;

namespace Basket.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IBasketRepository, BasketRepository>()
            .AddTransient<ISerializeService, SerializeService>();
        return services;
    }

    public static IServiceCollection ConfigureRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetValue<string>("Redis:ConnectionString");
        if (string.IsNullOrEmpty(redisConnectionString))
            throw new ArgumentNullException(nameof(redisConnectionString),
                "Redis connection string is not configured.");
        services.AddStackExchangeRedisCache(options => { options.Configuration = redisConnectionString; });
        return services;
    }

    public static IServiceCollection ConfigureEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        var eventBusSettings = configuration.GetSection("EventBusSettings").Get<EventBusSettings>();
        if (eventBusSettings == null || string.IsNullOrEmpty(eventBusSettings.HostAddress))
            throw new ArgumentNullException(nameof(eventBusSettings), "Event bus settings are not configured.");

        var mqHost = new Uri(eventBusSettings.HostAddress);
        services.AddSingleton(KebabCaseEndpointNameFormatter.Instance);
        services.AddMassTransit(config => { config.UsingRabbitMq((ctx, cfg) => { cfg.Host(mqHost); }); });
        return services;
    }

    public static IServiceCollection ConfigureGrpcServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var stockUrl = configuration.GetValue<string>("GrpcSettings:StockUrl");
        if (string.IsNullOrEmpty(stockUrl))
            throw new ArgumentNullException(nameof(stockUrl), "Stock url is not configured.");
        services.AddGrpcClient<StockProtoService.StockProtoServiceClient>(options =>
            options.Address = new Uri(stockUrl));

        services.AddScoped<StockItemGrpcService>();
        return services;
    }
}