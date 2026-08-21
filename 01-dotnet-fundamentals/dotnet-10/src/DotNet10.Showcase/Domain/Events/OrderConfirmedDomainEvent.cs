using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet10.Showcase.Domain.Events
{
    public sealed record OrderConfirmedDomainEvent(
        OrderId OrderId,
        DateTimeOffset OccurredAt);
}
