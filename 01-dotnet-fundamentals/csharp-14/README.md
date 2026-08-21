# C# 14 — C# / .NET Fundamentals

Subpasta: `01-dotnet-fundamentals/csharp-14`

Objetivo: consolidar os fundamentos do C# 14 e do .NET 10 por meio de um domínio de transações, aplicando modelagem, serviços, extensões, parsing e otimizações de baixo nível.

Conteúdo demonstrado (resumo):

- Records, enums, objetos de valor e entidades de domínio.
- Nullable Reference Types, validação e tratamento explícito de erros.
- Serviços de aplicação para parsing e processamento de transações.
- Métodos de extensão e organização por responsabilidades.
- `ReadOnlySpan<char>` para parsing com menor alocação.
- Testes unitários com xUnit.
- BenchmarkDotNet para medir o parser otimizado.

## Estrutura

- `src/CSharp14.Showcase`: aplicação console e domínio.
- `src/CSharp14.Showcase.Benchmarks`: benchmarks de performance.
- `tests/CSharp14.Showcase.Tests`: testes automatizados.

## Como usar

```powershell
dotnet build CSharp14.Showcase.slnx
dotnet run --project src/CSharp14.Showcase
dotnet test CSharp14.Showcase.slnx
dotnet run --project src/CSharp14.Showcase.Benchmarks -c Release
```

## Decisões de engenharia

O projeto separa domínio, aplicação, extensões e performance para manter responsabilidades claras. O uso de `Span` fica isolado no caminho de parsing, permitindo comparar legibilidade e custo de alocação sem contaminar o modelo de domínio.
