using DependencyInjection.Showcase.Application.Contracts;
using DependencyInjection.Showcase.Application.Models;
using DependencyInjection.Showcase.Application.Services;
using DependencyInjection.Showcase.Domain;
using DependencyInjection.Showcase.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace DependencyInjection.Showcase.Tests;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void CompositionRoot_ResolvesDecoratorFromContract()
    {
        using var provider = BuildProvider();

        using var scope = provider.CreateScope();
        var processor = scope.ServiceProvider.GetRequiredService<IOrderProcessor>();

        Assert.IsType<OrderProcessorDecorator>(processor);
        Assert.Equal("Processado", processor.Process(1001).Status);
    }

    [Fact]
    public void RegisteredLifetimes_AreRespected()
    {
        using var provider = BuildProvider();
        using var firstScope = provider.CreateScope();
        using var secondScope = provider.CreateScope();

        var firstRepository = firstScope.ServiceProvider.GetRequiredService<IOrderRepository>();
        var secondRepository = secondScope.ServiceProvider.GetRequiredService<IOrderRepository>();
        var firstContext = firstScope.ServiceProvider.GetRequiredService<OperationContext>();
        var secondContext = secondScope.ServiceProvider.GetRequiredService<OperationContext>();
        var firstProcessor = firstScope.ServiceProvider.GetRequiredService<IOrderProcessor>();
        var secondProcessor = secondScope.ServiceProvider.GetRequiredService<IOrderProcessor>();

        Assert.Same(firstRepository, secondRepository);
        Assert.NotSame(firstContext, secondContext);
        Assert.NotSame(firstProcessor, secondProcessor);
    }

    [Fact]
    public void OrderProcessor_CanUseSubstituteRepository()
    {
        var processor = new OrderProcessor(new FakeOrderRepository());

        var result = processor.Process(7);

        Assert.Equal(7, result.Id);
        Assert.Equal("Processado", result.Status);
    }

    [Fact]
    public void OrderProcessor_ThrowsWhenOrderDoesNotExist()
    {
        var processor = new OrderProcessor(new EmptyOrderRepository());

        Assert.Throws<KeyNotFoundException>(() => processor.Process(999));
    }

    private static ServiceProvider BuildProvider()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddShowcaseServices();

        return services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true,
            ValidateOnBuild = true
        });
    }

    private sealed class FakeOrderRepository : IOrderRepository
    {
        public Order? GetById(int id) => new(id, 100m);
    }

    private sealed class EmptyOrderRepository : IOrderRepository
    {
        public Order? GetById(int id) => null;
    }
}