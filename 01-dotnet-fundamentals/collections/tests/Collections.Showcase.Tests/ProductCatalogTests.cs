using Collections.Showcase.Application;
using Collections.Showcase.Domain;
using Xunit;

namespace Collections.Showcase.Tests;

public sealed class ProductCatalogTests
{
    [Fact]
    public void TryGetById_ReturnsProductFromIndex()
    {
        var catalog = CreateCatalog();

        var found = catalog.TryGetById(2, out var product);

        Assert.True(found);
        Assert.Equal("Mouse", product?.Name);
    }

    [Fact]
    public void Add_RejectsDuplicateId()
    {
        var catalog = CreateCatalog();

        var exception = Assert.Throws<InvalidOperationException>(() =>
            catalog.Add(new Product(1, "OTHER", "Outro", "Eletrônicos", 10m)));

        Assert.Contains("already exists", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void GetSnapshot_RemainsUnchangedAfterLaterAdd()
    {
        var catalog = CreateCatalog();
        var snapshot = catalog.GetSnapshot();

        catalog.Add(new Product(3, "CHAIR-001", "Cadeira", "Escritório", 950m));

        Assert.Equal(2, snapshot.Length);
        Assert.Equal(3, catalog.GetSnapshot().Length);
    }

    [Fact]
    public void FindByCategory_IsCaseInsensitive()
    {
        var catalog = CreateCatalog();

        var products = catalog.FindByCategory("ELETRÔNICOS").ToArray();

        Assert.Equal(2, products.Length);
    }

    [Fact]
    public void FindByCategory_RejectsBlankCategory()
    {
        var catalog = CreateCatalog();

        Assert.Throws<ArgumentException>(() => catalog.FindByCategory(" ").ToArray());
    }

    private static ProductCatalog CreateCatalog()
    {
        var catalog = new ProductCatalog();
        catalog.Add(new Product(1, "NOTE-001", "Notebook", "Eletrônicos", 4500m));
        catalog.Add(new Product(2, "MOUSE-001", "Mouse", "Eletrônicos", 120m));
        return catalog;
    }
}