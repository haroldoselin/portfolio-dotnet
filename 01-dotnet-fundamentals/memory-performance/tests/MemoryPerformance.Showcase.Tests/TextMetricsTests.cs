using MemoryPerformance.Showcase.Performance;
using Xunit;

namespace MemoryPerformance.Showcase.Tests;

public sealed class TextMetricsTests
{
    [Theory]
    [InlineData("um dois três", 3)]
    [InlineData("  espaços   repetidos ", 2)]
    [InlineData("", 0)]
    public void CountWords_ProducesSameResultAsBaseline(string text, int expected)
    {
        var baseline = TextMetrics.CountWordsBaseline(text);
        var optimized = TextMetrics.CountWords(text.AsSpan());

        Assert.Equal(expected, baseline);
        Assert.Equal(baseline, optimized);
    }

    [Fact]
    public async Task ComputeChecksumAsync_ProcessesReadOnlyMemory()
    {
        var processor = new PooledBufferProcessor();

        var checksum = await processor.ComputeChecksumAsync(new byte[] { 1, 2, 3 }.AsMemory());

        Assert.Equal(6, checksum);
    }

    [Fact]
    public void ComputeWithPooledBuffer_ReturnsExpectedChecksum()
    {
        var processor = new PooledBufferProcessor();

        var checksum = processor.ComputeWithPooledBuffer(new byte[] { 100, 100, 100 });

        Assert.Equal(44, checksum);
    }

    [Fact]
    public async Task ComputeChecksumAsync_HonorsCancellation()
    {
        var processor = new PooledBufferProcessor();
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();

        await Assert.ThrowsAsync<TaskCanceledException>(() =>
            processor.ComputeChecksumAsync(ReadOnlyMemory<byte>.Empty, cancellation.Token));
    }
}