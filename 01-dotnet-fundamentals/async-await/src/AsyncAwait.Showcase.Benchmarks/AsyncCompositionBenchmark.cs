using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class AsyncCompositionBenchmark
{
    [Params(10, 50)]
    public int DelayMilliseconds { get; set; }

    [Benchmark]
    public async Task<int> SequentialAsync()
    {
        var first = await ReadAsync();
        var second = await ReadAsync();
        return first + second;
    }

    [Benchmark]
    public async Task<int> WhenAllAsync()
    {
        var firstTask = ReadAsync();
        var secondTask = ReadAsync();
        await Task.WhenAll(firstTask, secondTask);
        return await firstTask + await secondTask;
    }

    private async Task<int> ReadAsync()
    {
        await Task.Delay(DelayMilliseconds);
        return 1;
    }
}