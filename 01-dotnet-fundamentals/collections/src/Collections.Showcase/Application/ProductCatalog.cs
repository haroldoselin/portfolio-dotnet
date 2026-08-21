using System.Collections.Immutable;
using Collections.Showcase.Domain;

namespace Collections.Showcase.Application;

public sealed class ProductCatalog
{
    private readonly Dictionary<int, Product> productsById = [];
    private ImmutableArray<Product> snapshot = [];

    public int Count => productsById.Count;

    public void Add(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (!productsById.TryAdd(product.Id, product))
        {
            throw new InvalidOperationException($"Id do produto '{product.Id}' já existe.");
        }

        snapshot = [.. productsById.Values];
    }

    public bool TryGetById(int id, out Product? product) =>
        productsById.TryGetValue(id, out product);

    public ImmutableArray<Product> GetSnapshot() => snapshot;

    public IEnumerable<Product> FindByCategory(string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);

        return snapshot.Where(product =>
            string.Equals(product.Category, category, StringComparison.OrdinalIgnoreCase));
    }

    public IReadOnlyDictionary<string, decimal> CalculateTotalsByCategory()
    {
        return snapshot
            .GroupBy(product => product.Category, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                group => group.Key,
                group => group.Sum(product => product.Price),
                StringComparer.OrdinalIgnoreCase);
    }
}