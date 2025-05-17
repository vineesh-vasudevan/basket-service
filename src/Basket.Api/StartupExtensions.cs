using Basket.Shared.Behaviors;
using Basket.Shared.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using RabbitMQ.Client;
using Serilog;
using System.Text.Json.Serialization;

namespace Basket.Api
{
    public static class StartupExtensions
    {
        private static readonly string[] tags = ["messagequeue"];

        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register services
            services.AddCarter();

            services.AddHttpContextAccessor();

            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CorrelationIdBehavior<,>));
            services.AddExceptionHandler<CustomExceptionHandler>();

            // Add Health Checks
            var dbConnection = configuration.GetConnectionString("BasketDb");
            var redisConnection = configuration.GetConnectionString("Redis");
            var rabbitMqConnection = configuration.GetSection("MessageBroker")["Connection"];

            services.AddHealthChecks()
                 .AddMySql(dbConnection!, name: "MySql")
                 .AddRabbitMQ(sp =>
                 {
                     var factory = new ConnectionFactory { Uri = new Uri(rabbitMqConnection!) };
                     return factory.CreateConnectionAsync();
                 }, name: "RabbitMQ", tags: tags)
                 .AddRedis(redisConnection!, name: "Redis");

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
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

            return app;
        }
    }
}