using Common.Logging;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services;
using Customer.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Starting {EnvironmentApplicationName} up", builder.Environment.ApplicationName);
try
{
// Add services to the container.

    builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<CustomerContext>(options =>
        options.UseNpgsql(connectionString!, optionsBuilder => optionsBuilder.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorCodesToAdd: null
        )));

    // Register the repository and service
    builder.Services
        // .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
        // .AddScoped(typeof(IRepositoryQueryBase<,,>), typeof(RepositoryBaseAsync<,,>))
        .AddScoped<ICustomerRepository, CustomerRepository>()
        .AddScoped<ICustomerService, CustomerService>();

    var app = builder.Build();

// Endpoint for the application
    app.MapGet("/", () => "Welcome to the Customer API!");
    app.MapGet("/api/customers/{userName}",
        async (string userName, ICustomerService customerService) =>
            await customerService.GetCustomerByName(userName));


// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.SeedCustomerData().Run();
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
    Log.CloseAndFlush();
}