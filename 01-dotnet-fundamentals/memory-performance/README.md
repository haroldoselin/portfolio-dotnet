# Memory Performance — C# / .NET Fundamentals

Subpasta: `01-dotnet-fundamentals/memory-performance`

Objetivo: demonstrar técnicas de performance e gerenciamento de memória em C# 14/.NET 10, sempre relacionando otimizações a medições e ao custo de manutenção.

Conteúdo demonstrado (resumo):

- Alocações no heap e impacto da coleta de lixo.
- `Span<T>`, `ReadOnlySpan<T>`, `Memory<T>` e `ReadOnlyMemory<T>`.
- `ArrayPool<T>` para buffers temporários.
- Evitar cópias e alocações desnecessárias.
- Comparação entre APIs orientadas a abstração e caminhos otimizados.
- BenchmarkDotNet e `MemoryDiagnoser`.
- Análise de throughput, latência e memória alocada.

## Estrutura

- A implementação do exemplo deve ficar em `src/MemoryPerformance.Showcase`.
- Os benchmarks devem ficar em `src/MemoryPerformance.Showcase.Benchmarks`.
- Os testes devem ficar em `tests/MemoryPerformance.Showcase.Tests`.

## Como usar

```powershell
dotnet build
dotnet test
dotnet run -c Release
```

## Decisões de engenharia

O projeto prioriza otimização orientada por evidência. APIs de baixo nível são isoladas, documentadas por testes e comparadas com uma implementação legível para evitar complexidade sem ganho mensurável.
