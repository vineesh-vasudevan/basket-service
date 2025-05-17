using Basket.Domain.Messaging;
using Basket.Domain.Repositories;
using Basket.Infrastructure.Data;
using Basket.Infrastructure.Messaging;
using Basket.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnection = configuration.GetConnectionString("BasketDb");
            var redisConnection = configuration.GetConnectionString("Redis");

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection)));

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) =>
                {
                    var host = configuration["MessageBroker:Host"];
                    var username = configuration["MessageBroker:UserName"];
                    var password = configuration["MessageBroker:Password"];

                    cfg.Host(new Uri(host!), h =>
                    {
                        h.Username(username!);
                        h.Password(password!);
                    });
                });
            });

            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketEventPublisher, BasketEventPublisher>();

            services.Decorate<IBasketRepository, CacheBasketRepository>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnection;
            });

            return services;
        }
    }
}