using System.Buffers;
using Collections.Showcase.Application;
using Collections.Showcase.Domain;

var catalog = new ProductCatalog();

foreach (var product in new[]
{
    new Product(1, "NOTE-001", "Notebook", "Eletrônicos", 4500m),
    new Product(2, "MOUSE-001", "Mouse", "Eletrônicos", 120m),
    new Product(3, "CHAIR-001", "Cadeira", "Escritório", 950m)
})
{
    catalog.Add(product);
}

Console.WriteLine("======================================");
Console.WriteLine(" COLLECTIONS - .NET 10 SENIOR SHOWCASE");
Console.WriteLine("======================================");
Console.WriteLine($"Produtos cadastrados: {catalog.Count}");

if (catalog.TryGetById(2, out var productById) && productById is not null)
{
    Console.WriteLine($"Busca indexada: {productById.Name} | {productById.Price:C}");
}

Console.WriteLine("Produtos de Eletrônicos:");
foreach (var product in catalog.FindByCategory("eletrônicos"))
{
    Console.WriteLine($"- {product.Sku}: {product.Name}");
}

Console.WriteLine("Totais por categoria:");
foreach (var (category, total) in catalog.CalculateTotalsByCategory())
{
    Console.WriteLine($"- {category}: {total:C}");
}

var snapshot = catalog.GetSnapshot();
Console.WriteLine($"Snapshot imutável: {snapshot.Length} produtos");

var rentedBuffer = ArrayPool<byte>.Shared.Rent(1024);
try
{
    rentedBuffer.AsSpan(0, 4).Clear();
    Console.WriteLine($"Buffer temp reutilizado: {rentedBuffer.Length} bytes");
}
finally
{
    ArrayPool<byte>.Shared.Return(rentedBuffer, clearArray: true);
}