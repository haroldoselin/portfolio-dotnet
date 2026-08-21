# Async/Await — C# / .NET Fundamentals

Subpasta: `01-dotnet-fundamentals/async-await`

Objetivo: demonstrar programação assíncrona em C# 14 e .NET 10, com foco em responsividade, uso correto de `Task`, cancelamento e composição de operações de I/O.

Conteúdo demonstrado (resumo):

- `async`/`await` e composição de tarefas.
- `Task` e `ValueTask` em cenários apropriados.
- `CancellationToken` e cancelamento cooperativo.
- Execução concorrente com `Task.WhenAll`.
- Tratamento de exceções em fluxos assíncronos.
- Evitar bloqueios com `.Result` e `.Wait()`.
- Testabilidade de serviços assíncronos.

## Estrutura

- A implementação do exemplo deve ficar em `src/AsyncAwait.Showcase`.
- Os testes devem ficar em `tests/AsyncAwait.Showcase.Tests`.

## Como usar

```powershell
dotnet build
dotnet run
dotnet test
```

## Decisões de engenharia

O fluxo assíncrono deve preservar o `CancellationToken`, evitar bloqueio de threads e propagar falhas de forma observável. `Task.WhenAll` deve ser usado somente quando as operações forem independentes e puderem ocorrer em paralelo.
