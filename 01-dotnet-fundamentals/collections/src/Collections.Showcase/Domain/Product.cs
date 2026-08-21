namespace Collections.Showcase.Domain;

public sealed record Product(
    int Id,
    string Sku,
    string Name,
    string Category,
    decimal Price);