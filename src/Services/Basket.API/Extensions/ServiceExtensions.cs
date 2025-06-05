using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Infrastructure.Common;
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
}