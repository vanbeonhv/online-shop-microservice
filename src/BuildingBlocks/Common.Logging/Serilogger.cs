using Microsoft.Extensions.Hosting;
using Serilog;

namespace Common.Logging;

public static class Serilogger
{
    public static Action<HostBuilderContext, LoggerConfiguration> Configure => (context, configuration) =>
    {
        var applicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", ",");
        var environmentName = context.HostingEnvironment.EnvironmentName ?? "Development";

        configuration.WriteTo.Debug()
            .WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}{NewLine} {Message:lj}{NewLine}{Exception}"
            ).ReadFrom.Configuration(context.Configuration);
    };
}