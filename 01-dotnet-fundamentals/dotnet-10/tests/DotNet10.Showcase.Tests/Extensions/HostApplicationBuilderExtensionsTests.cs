using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using DotNet10.Showcase.Extensions;
using Xunit;

namespace DotNet10.Showcase.Tests.Extensions
{
    public sealed class HostApplicationBuilderExtensionsTests
    {
        [Fact]
        public void AddShowcaseServices_Registers_All_Services()
        {
            // Arrange
            var builder = new HostApplicationBuilder();

            // Act
            builder.AddShowcaseServices();

            var provider = builder.Services.BuildServiceProvider();

            // Assert - basic check that services were registered
            var service = provider.GetService<DotNet10.Showcase.Application.Contracts.IOrderService>();
            var repo = provider.GetService<DotNet10.Showcase.Application.Contracts.IOrderRepository>();

            Assert.NotNull(service);
            Assert.NotNull(repo);
        }
    }
}
