using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using DotNet10.Showcase.Extensions;
using DotNet10.Showcase.Infrastructure.Configuration;
using DotNet10.Showcase.Application.Contracts;
using DotNet10.Showcase.Infrastructure.Persistence;
using DotNet10.Showcase.Infrastructure.Time;
using Xunit;

namespace DotNet10.Showcase.Tests.Extensions
{
    public sealed class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddInfrastructure_Registers_Singletons_And_Binds_Options()
        {
            // Arrange
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    [$"{OrderProcessingOptions.SectionName}:BatchSize"] = "5",
                    [$"{OrderProcessingOptions.SectionName}:PollingIntervalSeconds"] = "1"
                })
                .Build();

            var services = new ServiceCollection();

            // Act
            services.AddInfrastructure(config);
            var provider = services.BuildServiceProvider();

            // Assert
            var repo = provider.GetRequiredService<IOrderRepository>();
            Assert.IsType<InMemoryOrderRepository>(repo);

            var clock = provider.GetRequiredService<ISystemClock>();
            Assert.IsType<SystemClock>(clock);

            var options = provider.GetRequiredService<IOptions<OrderProcessingOptions>>();
            Assert.Equal(5, options.Value.BatchSize);
            Assert.Equal(1, options.Value.PollingIntervalSeconds);
        }

        [Fact]
        public void AddApplication_Registers_OrderService()
        {
            var services = new ServiceCollection();

            // OrderService depends on repository and clock from infrastructure, so register infrastructure first
            var config = new ConfigurationBuilder().Build();
            services.AddInfrastructure(config);
            services.AddApplication();

            var provider = services.BuildServiceProvider();

            var service = provider.GetService<IOrderService>();

            Assert.NotNull(service);
        }
    }
}
