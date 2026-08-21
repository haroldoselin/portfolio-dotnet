using BenchmarkDotNet.Attributes;
using MemoryPerformance.Showcase.Performance;

[MemoryDiagnoser]
public class TextMetricsBenchmark
{
    private string text = string.Empty;

    [GlobalSetup]
    public void Setup()
    {
        text = string.Join(' ', Enumerable.Repeat("dotnet-performance", 500));
    }

    [Benchmark(Baseline = true)]
    public int SplitBased() => TextMetrics.CountWordsBaseline(text);

    [Benchmark]
    public int SpanBased() => TextMetrics.CountWords(text.AsSpan());
}