using DependencyInjection.Showcase.Application.Contracts;
using DependencyInjection.Showcase.Application.Models;
using DependencyInjection.Showcase.Application.Services;
using DependencyInjection.Showcase.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.Showcase.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShowcaseServices(this IServiceCollection services)
    {
        services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
        services.AddScoped<OperationContext>();
        services.AddTransient<OrderProcessor>();
        services.AddTransient<IOrderProcessor, OrderProcessorDecorator>();

        return services;
    }
}