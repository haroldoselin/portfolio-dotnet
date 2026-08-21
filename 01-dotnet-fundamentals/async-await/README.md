# Async/Await — C# / .NET Fundamentals

Subpasta: `01-dotnet-fundamentals/async-await`

Objetivo: demonstrar programação assíncrona em C# 14 e .NET 10 por meio de um serviço de resumo de pedidos, com foco em responsividade, composição de tarefas, cancelamento cooperativo e tratamento observável de falhas.

Conteúdo demonstrado (resumo):

- `async`/`await` para operações de I/O sem bloqueio de threads.
- `Task` e `ValueTask` em caminhos apropriados.
- `CancellationToken` propagado até a fonte de dados.
- `Task.WhenAll` para consultas independentes em paralelo.
- Propagação de exceções sem `.Result` ou `.Wait()`.
- `TimeProvider` para facilitar testes determinísticos.
- Testes unitários de sucesso, concorrência, cancelamento e falhas.
- BenchmarkDotNet comparando composição sequencial e concorrente.

## Estrutura

- `src/AsyncAwait.Showcase`: aplicação console, contratos, serviço e fonte de dados em memória.
- `src/AsyncAwait.Showcase.Benchmarks`: benchmark de composição assíncrona.
- `tests/AsyncAwait.Showcase.Tests`: testes automatizados com xUnit.

## Como usar

```powershell
dotnet build AsyncAwait.Showcase.slnx
dotnet run --project src/AsyncAwait.Showcase
dotnet test AsyncAwait.Showcase.slnx
dotnet run --project src/AsyncAwait.Showcase.Benchmarks -c Release
```

## Decisões de engenharia

O serviço inicia as consultas independentes antes de aguardar `Task.WhenAll`, reduzindo a latência total. O token percorre todas as camadas até `Task.Delay`, enquanto exceções são propagadas ao chamador para que a borda da aplicação decida como registrar, retornar ou repetir a operação.
