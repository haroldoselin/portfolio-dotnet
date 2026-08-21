using DotNet10.Showcase.Domain.Enuns;
using DotNet10.Showcase.Domain.ValueObjects;

namespace DotNet10.Showcase.Application.Models
{
    public sealed record OrderResponse(
        OrderId Id,
        string CustomerId,
        decimal Total,
        OrderStatus Status,
        DateTimeOffset CreatedAt);
}
