using Serilog.Events;
using Serilog;
using Basket.Application.Common.Mapping;
using Basket.Shared.Behaviors;
using Basket.Shared.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Basket.Application.Basket.CreateBasket;
using Basket.Infrastructure;
using static CSharpFunctionalExtensions.Result;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .CreateBootstrapLogger();
try
{
    Log.Information("Engine starting up...");

    var assembly = typeof(CreateBasketCommand).Assembly;
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

    // Register services
    builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
    builder.Services.AddCarter();
    builder.Services.AddValidatorsFromAssembly(assembly);

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CorrelationIdBehavior<,>));
    builder.Services.AddExceptionHandler<CustomExceptionHandler>();
    builder.Services.AddMediatR(config =>
    {
        config.RegisterServicesFromAssembly(assembly);
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));

    });

    builder.Services.AddInfrastructure(builder.Configuration!);


    // Add Health Checks
    var dbConnection = builder.Configuration.GetConnectionString("BasketDb");
    var redisConnection = builder.Configuration.GetConnectionString("Redis");

    builder.Services.AddHealthChecks();
    builder.Services.AddHealthChecks()
         .AddMySql(dbConnection!, name: "mysql");
    builder.Services.AddHealthChecks()
         .AddRedis(redisConnection!, name: "redis");

    var app = builder.Build();

    // Add structured request logging and include correlation ID
    app.UseSerilogRequestLogging(options =>
    {
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            if (httpContext.Request.Headers.TryGetValue("X-Correlation-Id", out var correlationId))
            {
                diagnosticContext.Set("CorrelationId", correlationId.ToString());
            }
        };
    });

    app.MapCarter();

    // Add exception handling middleware --> uses CustomExceptionHandler
    app.UseExceptionHandler(options => { });

    app.UseHealthChecks("/health",
        new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

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



