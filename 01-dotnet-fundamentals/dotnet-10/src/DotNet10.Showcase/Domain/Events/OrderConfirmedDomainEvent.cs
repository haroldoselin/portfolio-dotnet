using DotNet10.Showcase.Domain.ValueObjects;

namespace DotNet10.Showcase.Domain.Events
{
    public sealed record OrderConfirmedDomainEvent(
        OrderId OrderId,
        DateTimeOffset OccurredAt);
}
