using BenchmarkDotNet.Attributes;
using Collections.Showcase.Application;
using Collections.Showcase.Domain;

[MemoryDiagnoser]
public class ProductLookupBenchmark
{
    private ProductCatalog catalog = null!;

    [GlobalSetup]
    public void Setup()
    {
        catalog = new ProductCatalog();

        for (var id = 1; id <= 10_000; id++)
        {
            catalog.Add(new Product(id, $"SKU-{id}", $"Produto {id}", "Geral", id));
        }
    }

    [Benchmark]
    public bool DictionaryLookup() => catalog.TryGetById(9_999, out _);

    [Benchmark]
    public Product? SequentialLookup() =>
        catalog.GetSnapshot().FirstOrDefault(product => product.Id == 9_999);
}