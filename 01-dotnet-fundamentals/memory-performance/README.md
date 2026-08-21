# Memory Performance — C# / .NET Fundamentals

Subpasta: `01-dotnet-fundamentals/memory-performance`

Objetivo: demonstrar técnicas de performance e gerenciamento de memória em C# 14/.NET 10 por meio de métricas de texto e processamento de buffers, sempre relacionando otimizações a medições e ao custo de manutenção.

Conteúdo demonstrado (resumo):

- Comparação entre `string.Split` e `ReadOnlySpan<char>` para contagem de palavras.
- `ReadOnlyMemory<byte>` em uma API assíncrona de processamento.
- `ArrayPool<byte>` para reutilização de buffers temporários.
- Devolução segura de buffers com `finally` e limpeza do conteúdo alugado.
- Redução de cópias e alocações desnecessárias em caminhos quentes.
- BenchmarkDotNet com `MemoryDiagnoser` para medir memória alocada.
- Testes unitários para validar equivalência funcional e cancelamento.

## Estrutura

- `src/MemoryPerformance.Showcase`: aplicação console e APIs de performance.
- `src/MemoryPerformance.Showcase.Benchmarks`: benchmarks de texto.
- `tests/MemoryPerformance.Showcase.Tests`: testes automatizados com xUnit.

## Como usar

```powershell
dotnet build MemoryPerformance.Showcase.slnx
dotnet run --project src/MemoryPerformance.Showcase
dotnet test MemoryPerformance.Showcase.slnx
dotnet run --project src/MemoryPerformance.Showcase.Benchmarks -c Release
```

## Decisões de engenharia

O projeto mantém uma implementação baseline legível para comparação com o caminho baseado em `Span`. A otimização é isolada e validada por testes, enquanto o `MemoryDiagnoser` fornece evidência sobre alocações e tempo antes de qualquer decisão de adoção.

`ArrayPool` é usado apenas durante o processamento temporário e o buffer é devolvido em `finally`, inclusive em caso de falha. Em produção, o tamanho, a limpeza e o ciclo de vida do buffer devem ser avaliados conforme sensibilidade dos dados e perfil de carga.
