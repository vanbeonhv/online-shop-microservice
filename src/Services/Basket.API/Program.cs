using Basket.API.Extensions;
using Common.Logging;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Starting {EnvironmentApplicationName} up", builder.Environment.ApplicationName);
try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfiguration();
// Add services to the container.
    builder.Services.AddInfrastructure();
    builder.Services.ConfigureRedisCache(builder.Configuration);
    

    builder.Services.AddControllers();
    builder.Services.Configure<RouteOptions>(option => option.LowercaseUrls = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
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