namespace AsyncAwait.Showcase.Application.Models;

public sealed record OrderSummary(
    int CustomerId,
    decimal TotalAmount,
    int OrderCount,
    DateTimeOffset GeneratedAt);