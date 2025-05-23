﻿using Basket.Application.Basket.CreateBasket;
using Basket.Application.MappingProfile;
using Basket.Shared.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(CreateBasketCommand).Assembly;
            services.AddAutoMapper(typeof(BasketItemEventMappingProfile).Assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            return services;
        }
    }
}