using Ocelot.DependencyInjection;

namespace OcelotApiGateway.Extensions;

public static class ServiceExtensions
{
    internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }

    internal static void AddConfigureOcelot(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOcelot(configuration);
    }

    internal static void AddConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration["AllowedOrigins"];
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}