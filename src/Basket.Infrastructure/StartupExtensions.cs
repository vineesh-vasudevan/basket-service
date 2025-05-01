using Basket.Infrastructure.Data;
using Basket.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Basket.Domain.Repositories;
using Microsoft.Extensions.Configuration;

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

            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            services.Decorate<IBasketRepository, CacheBasketRepository>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnection;
            });

           
            return services;
        }
    }
}
