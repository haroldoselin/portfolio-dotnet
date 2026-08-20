```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.9168/25H2/2025Update/HudsonValley2)
Intel Core i5-10310U CPU 1.70GHz (Max: 2.21GHz), 1 CPU, 8 logical and 4 physical cores
.NET SDK 10.0.400
  [Host]     : .NET 10.0.11 (10.0.11, 10.0.1126.37416), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.11 (10.0.11, 10.0.1126.37416), X64 RyuJIT x86-64-v3


```
| Method        | Mean     | Error    | StdDev   | Allocated |
|-------------- |---------:|---------:|---------:|----------:|
| StringParsing | 68.25 ns | 1.527 ns | 4.205 ns |         - |
| SpanParsing   | 55.34 ns | 1.152 ns | 3.193 ns |         - |
