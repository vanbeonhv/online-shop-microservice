using MassTransit;
using Ordering.Infrastructure.Messaging.Consumers;
using Shared.Configurations;

namespace Ordering.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        var eventBusSettings = configuration.GetSection("EventBusSettings").Get<EventBusSettings>();
        if (eventBusSettings == null || string.IsNullOrEmpty(eventBusSettings.HostAddress))
            throw new ArgumentNullException(nameof(configuration), "Event bus settings are not configured.");

        var mqHost = new Uri(eventBusSettings.HostAddress);
        services.AddSingleton(KebabCaseEndpointNameFormatter.Instance);
        services.AddMassTransit(config =>
        {
            config.AddConsumer<BasketCheckoutConsumer>();
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(mqHost);
                cfg.ReceiveEndpoint("basket-checkout-queue",
                    e => { e.ConfigureConsumer<BasketCheckoutConsumer>(ctx); });
            });
        });
        return services;
    }
}