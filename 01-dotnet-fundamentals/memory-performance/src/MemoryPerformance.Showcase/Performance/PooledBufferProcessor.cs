using System.Buffers;

namespace MemoryPerformance.Showcase.Performance;

public sealed class PooledBufferProcessor
{
    public async Task<int> ComputeChecksumAsync(
        ReadOnlyMemory<byte> input,
        CancellationToken cancellationToken = default)
    {
        await Task.Delay(1, cancellationToken).ConfigureAwait(false);

        var checksum = 0;
        foreach (var value in input.Span)
        {
            checksum = (checksum + value) % 256;
        }

        return checksum;
    }

    public int ComputeWithPooledBuffer(ReadOnlySpan<byte> input)
    {
        var buffer = ArrayPool<byte>.Shared.Rent(input.Length);

        try
        {
            input.CopyTo(buffer);
            return ComputeChecksum(buffer.AsSpan(0, input.Length));
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer, clearArray: true);
        }
    }

    private static int ComputeChecksum(ReadOnlySpan<byte> input)
    {
        var checksum = 0;
        foreach (var value in input)
        {
            checksum = (checksum + value) % 256;
        }

        return checksum;
    }
}