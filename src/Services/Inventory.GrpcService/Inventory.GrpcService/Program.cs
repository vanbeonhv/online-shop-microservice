using Common.Logging;
using Inventory.GrpcService.Extensions;
using Inventory.GrpcService.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Starting Inventory gRPC up");

try
{
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddGrpc();

    var app = builder.Build();

// Configure the HTTP request pipeline.
    app.MapGrpcService<InventoryGrpcService>();
    app.MapGet("/",
        () =>
            "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

    await app.RunAsync();
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
    await Log.CloseAndFlushAsync();
}