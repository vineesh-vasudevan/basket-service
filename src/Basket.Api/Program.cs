using Basket.Api;
using Basket.Application;
using Basket.Infrastructure;
using Basket.Infrastructure.Data;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .CreateBootstrapLogger();
try
{
    Log.Information("Engine starting up...");

    var builder = WebApplication.CreateBuilder(args);

    // Configure Serilog with full settings
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console();
    });

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddApiServices(builder.Configuration);

    var app = builder.Build();

    app.UseApiServices();

    if (app.Environment.IsDevelopment())
    {
        await app.InitializeDatabaseAsync();
    }

    app.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Application startup failed!");
    throw;
}
finally
{
    Log.CloseAndFlush();
}