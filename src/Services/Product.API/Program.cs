using Common.Logging;
using Product.API.Extensions;
using Product.API.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Starting {EnvironmentApplicationName} up", builder.Environment.ApplicationName);
try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfiguration();
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();
    app.UseInfrastructure(builder.Environment);

    app.MigrationDatabase<ProductContext>((context, _) =>
    {
        ProductContextSeed.SeedProductAsync(context, Log.Logger).Wait();
    }).Run();
}
catch (Exception e)
{
    var type = e.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(e, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Product API");
    Log.CloseAndFlush();
}