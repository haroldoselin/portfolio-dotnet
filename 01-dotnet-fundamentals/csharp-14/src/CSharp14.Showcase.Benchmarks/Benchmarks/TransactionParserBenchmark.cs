using BenchmarkDotNet.Attributes;
using CSharp14.Showcase.Performance;

namespace CSharp14.Showcase.Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    public class TransactionParserBenchmark
    {
        private const string Input = "150.75";

        [Benchmark]
        public decimal StringParsing()
        {
            return TransactionParser.ParseWithString(Input);
        }

        [Benchmark]
        public decimal SpanParsing()
        {
            return TransactionParser.ParseWithSpan(Input);
        }
    }
}
