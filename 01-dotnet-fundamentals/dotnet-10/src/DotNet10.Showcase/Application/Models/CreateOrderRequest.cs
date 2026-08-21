namespace DotNet10.Showcase.Application.Models
{
    public sealed record CreateOrderRequest(
        string CustomerId,
        decimal Total);
}
