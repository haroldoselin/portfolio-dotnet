using DotNet10.Showcase.Application.Contracts;
using DotNet10.Showcase.Application.Services;
using DotNet10.Showcase.Hosting.HostedServices;
using DotNet10.Showcase.Infrastructure.Configuration;
using DotNet10.Showcase.Infrastructure.Persistence;
using DotNet10.Showcase.Infrastructure.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet10.Showcase.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<OrderProcessingOptions>(
                configuration.GetSection(
                    OrderProcessingOptions.SectionName));

            services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();

            services.AddSingleton<ISystemClock, SystemClock>();

            return services;
        }

        public static IServiceCollection AddHosting(
            this IServiceCollection services)
        {
            services.AddHostedService<OrderProcessingWorker>();

            return services;
        }
    }
}
