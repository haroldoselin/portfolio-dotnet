namespace DependencyInjection.Showcase.Application.Models;

public sealed class OperationContext
{
    public Guid CorrelationId { get; } = Guid.NewGuid();
}