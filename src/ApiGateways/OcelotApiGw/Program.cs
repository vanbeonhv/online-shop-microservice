using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotApiGateway.Extensions;
using OcelotApiGateway.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Starting {EnvironmentApplicationName} up", builder.Environment.ApplicationName);
try
{
    builder.Host.AddAppConfiguration();
    builder.Services.AddConfigurationSettings(builder.Configuration);
    builder.Services.AddConfigureCors(builder.Configuration);
    builder.Services.AddOcelot(builder.Configuration);

    // Add services to the container.
    builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1"));
    }

    app.UseCors("CorsPolicy");

    app.UseMiddleware<ErrorWrappingMiddleware>();
    await app.UseOcelot();
    app.UseAuthorization();

    // app.UseHttpsRedirection();


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