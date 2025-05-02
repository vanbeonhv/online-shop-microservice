using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Product.API.Persistence;

namespace Product.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.Configure<RouteOptions>(option => option.LowercaseUrls = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureProductDbContext(configuration);
        services.AddInfrastructureServices();

        return services;
    }

    private static IServiceCollection ConfigureProductDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var builder = new MySqlConnectionStringBuilder(connectionString ?? string.Empty);

        services.AddDbContext<ProductContext>(m =>
            m.UseMySql(builder.ConnectionString, ServerVersion.AutoDetect(builder.ConnectionString),
                b =>
                {
                    b.MigrationsAssembly(typeof(ProductContext).Assembly.FullName);
                    b.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                }));
        return services;
    }

    private static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        return services
            .AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>))
            .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
    }
}