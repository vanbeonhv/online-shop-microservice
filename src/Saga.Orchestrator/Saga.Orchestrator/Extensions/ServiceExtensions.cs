using Saga.Orchestrator.HttpRepository;
using Saga.Orchestrator.HttpRepository.Interfaces;
using Saga.Orchestrator.Services;
using Saga.Orchestrator.Services.Interfaces;

namespace Saga.Orchestrator.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureService(this IServiceCollection services)
    {
        services.AddScoped<ICheckoutSageService, CheckoutSageSagaService>();
        return services;
    }

    public static IServiceCollection ConfigureHttpRepository(this IServiceCollection services)
    {
        services.AddScoped<IBasketHttpRepository, BasketHttpRepository>();
        services.AddScoped<IOrderHttpRepository, OrderHttpRepository>();
        services.AddScoped<IInventoryHttpRepository, InventoryHttpRepository>();

        return services;
    }

    private static void ConfigureOrderHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IOrderHttpRepository, OrderHttpRepository>(name: "OrdersAPI",
            configureClient: (sp, cl) => { cl.BaseAddress = new Uri("http://localhost:5005/api/v1"); });
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
            .CreateClient("OrdersAPI"));
    }

    private static void ConfigureBasketHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IBasketHttpRepository, BasketHttpRepository>(name: "BasketsAPI",
            configureClient: (sp, cl) => { cl.BaseAddress = new Uri("http://localhost:5004/api"); });
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
            .CreateClient("BasketsAPI"));
    }
    
    private static void ConfigureInventoryHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient<IInventoryHttpRepository, InventoryHttpRepository>(name: "InventoryAPI", configureClient: (sp, cl) =>
        {
            cl.BaseAddress = new Uri("http://localhost:5006/api");
        });
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
            .CreateClient("InventoryAPI"));
    }
}