using DotNet10.Showcase.Domain.ValueObjects;

namespace DotNet10.Showcase.Domain.Events
{
    public sealed record OrderCreatedDomainEvent(
        OrderId OrderId,
        string CustomerId,
        DateTimeOffset OccurredAt);
}
