using MemoryPerformance.Showcase.Performance;

const string text = "Performance orientada por evidências no .NET 10";
var input = new byte[] { 10, 20, 30, 40, 50 };

Console.WriteLine("============================================");
Console.WriteLine(" MEMORY PERFORMANCE - .NET 10 SHOWCASE");
Console.WriteLine("============================================");

var baselineWords = TextMetrics.CountWordsBaseline(text);
var optimizedWords = TextMetrics.CountWords(text.AsSpan());

Console.WriteLine($"Baseline (string.Split): {baselineWords} palavras");
Console.WriteLine($"Otimizado (ReadOnlySpan): {optimizedWords} palavras");

var processor = new PooledBufferProcessor();
var checksum = await processor.ComputeChecksumAsync(input.AsMemory());
var pooledChecksum = processor.ComputeWithPooledBuffer(input);

Console.WriteLine($"Checksum (ReadOnlyMemory): {checksum}");
Console.WriteLine($"Checksum (ArrayPool): {pooledChecksum}");