using Microsoft.EntityFrameworkCore;

namespace Product.API.Extensions;

public static class HostExtensions
{
    public static IHost MigrationDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
        where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetRequiredService<TContext>();
        try
        {
            logger.LogInformation("Migrating mysql database");
            context.Database.Migrate();
            logger.LogInformation("Migrated mysql database");
            seeder(context, services);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occured while migrating the sql database");
        }

        return host;
    }
}