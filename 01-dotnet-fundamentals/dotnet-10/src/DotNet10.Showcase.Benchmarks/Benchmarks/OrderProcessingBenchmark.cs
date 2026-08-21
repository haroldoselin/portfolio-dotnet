using BenchmarkDotNet.Attributes;

namespace DotNet10.Showcase.Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    public class OrderProcessingBenchmark
    {
        private readonly int[] _orders =
            [.. Enumerable.Range(1, 10_000)];

        [Benchmark]
        public int LinqProcessing()
        {
            return _orders
                .Where(order => order % 2 == 0)
                .Select(order => order * 2)
                .Sum();
        }

        [Benchmark]
        public int LoopProcessing()
        {
            var total = 0;

            foreach (var order in _orders)
            {
                if (order % 2 == 0)
                {
                    total += order * 2;
                }
            }

            return total;
        }
    }
}
