using Common.Logging;
using Serilog;

namespace OcelotApiGateway.Extensions;

public static class HostExtensions
{
    public static void AddAppConfiguration(this ConfigureHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;
            config.AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddJsonFile($"ocelot.{env.EnvironmentName}.json", false, true)
                .AddEnvironmentVariables();
        });
        host.UseSerilog(Serilogger.Configure);
    }
}