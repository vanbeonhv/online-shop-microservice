using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

public static class OrderContextSeed
{
    public static async Task<IHost> SeedOrderData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var orderContext = scope.ServiceProvider
            .GetRequiredService<OrderContext>();
        orderContext.Database.MigrateAsync();

        await CreateOrder(orderContext);

        return host;
    }

    private static async Task CreateOrder(OrderContext orderContext)
    {
        var order = await orderContext.Order.AnyAsync();
        if (!order)
        {
            var newOrder = new Order()
            {
                UserName = "marc",
                FirstName = "marc",
                LastName = "marc",
                EmailAddress = "marc@gmail.com",
                ShippingAddress = "ABC Building",
                InvoiceAddress = "Hong Kong",
                TotalPrice = 250
            };
            await orderContext.Order.AddAsync(newOrder);
            await orderContext.SaveChangesAsync();
        }
    }
}