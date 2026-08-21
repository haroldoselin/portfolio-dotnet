# Collections — C# / .NET Fundamentals

Subpasta: `01-dotnet-fundamentals/collections`

Objetivo: demonstrar, em um cenário de catálogo de produtos, como escolher e utilizar coleções do C# 14/.NET 10 com foco em legibilidade, segurança, concorrência e performance.

Conteúdo demonstrado (resumo):

- `Dictionary<TKey, TValue>` para busca indexada por identificador, com custo médio O(1).
- `ImmutableArray<T>` para expor snapshots que não podem ser alterados pelo consumidor.
- `List<T>`, arrays e LINQ para composição, filtragem e agregação de dados.
- Comparação case-insensitive com `StringComparer.OrdinalIgnoreCase`.
- Validação de entradas e rejeição explícita de identificadores duplicados.
- `ArrayPool<T>` para reutilizar buffers temporários e reduzir alocações no GC.
- Benchmark com BenchmarkDotNet comparando lookup indexado e busca sequencial.
- Testes unitários com xUnit para comportamento, imutabilidade e regras de validação.

## Estrutura

- `src/Collections.Showcase`: aplicação console, catálogo e modelo de domínio.
- `src/Collections.Showcase.Benchmarks`: benchmarks de acesso às coleções.
- `tests/Collections.Showcase.Tests`: testes automatizados.

## Como usar

```powershell
dotnet build Collections.Showcase.slnx
dotnet run --project src/Collections.Showcase
dotnet test Collections.Showcase.slnx
dotnet run --project src/Collections.Showcase.Benchmarks -c Release
```

## Decisões de engenharia

O catálogo mantém um `Dictionary` privado para consultas por ID e reconstrói um snapshot imutável quando ocorre uma inclusão. Dessa forma, o caminho de leitura não expõe a coleção mutável interna e o consumidor recebe uma visão estável dos dados. O benchmark evidencia quando uma estrutura indexada é preferível a uma varredura linear.