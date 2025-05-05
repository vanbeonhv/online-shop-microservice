using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Infrastructure.Common;

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
}