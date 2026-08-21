using DotNet10.Showcase.Domain.ValueObjects;

namespace DotNet10.Showcase.Application.Models
{
    public sealed record ProcessingResult(
        OrderId OrderId,
        bool Success,
        string Message);
}
