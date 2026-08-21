using BenchmarkDotNet.Attributes;
using DependencyInjection.Showcase.Application.Contracts;
using DependencyInjection.Showcase.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[MemoryDiagnoser]
public class DependencyInjectionBenchmark
{
    private ServiceProvider provider = null!;
    private IServiceScope scope = null!;
    private IOrderProcessor processor = null!;

    [GlobalSetup]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddShowcaseServices();
        provider = services.BuildServiceProvider();
        scope = provider.CreateScope();
        processor = scope.ServiceProvider.GetRequiredService<IOrderProcessor>();
    }

    [Benchmark]
    public int ProcessOrder() => processor.Process(1001).Id;

    [GlobalCleanup]
    public void Cleanup()
    {
        scope.Dispose();
        provider.Dispose();
    }
}