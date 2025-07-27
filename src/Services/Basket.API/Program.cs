using Basket.API;
using Basket.API.Extensions;
using Common.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.Host.AddAppConfiguration();
// Add services to the container.
    builder.Services.AddInfrastructure();
    builder.Services.ConfigureRedisCache(builder.Configuration);
    builder.Services.ConfigureEventBus(builder.Configuration);
    builder.Services.ConfigureGrpcServices(builder.Configuration);
    builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));


    builder.Services.AddControllers();
    builder.Services.Configure<RouteOptions>(option => option.LowercaseUrls = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    Log.Information("Starting {EnvironmentApplicationName} up", builder.Environment.ApplicationName);

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
    Log.Information("Shut down {EnvironmentApplicationName}", builder.Environment.ApplicationName);
    await Log.CloseAndFlushAsync();
}