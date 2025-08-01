using Saga.Orchestrator.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.Host.AddAppConfiguration();
    // builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers();

    var app = builder.Build();
    Log.Information("Starting {EnvironmentApplicationName} up", builder.Environment.ApplicationName);

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

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
    Log.Information("Shut down {EnvironmentApplicationName}", builder.Environment.ApplicationName);
    await Log.CloseAndFlushAsync();
}