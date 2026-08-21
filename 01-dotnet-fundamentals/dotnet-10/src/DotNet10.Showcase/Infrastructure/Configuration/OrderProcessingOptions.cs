namespace DotNet10.Showcase.Infrastructure.Configuration
{
    public sealed class OrderProcessingOptions
    {
        public const string SectionName = "Pedido em processamenro";

        public int BatchSize { get; init; } = 10;

        public int PollingIntervalSeconds { get; init; } = 5;

        public int MaxAttempts { get; init; } = 3;
    }
}
