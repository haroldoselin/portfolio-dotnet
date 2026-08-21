# Dependency Injection — C# / .NET Fundamentals

Subpasta: `01-dotnet-fundamentals/dependency-injection`

Objetivo: demonstrar Dependency Injection no .NET 10, com composição de serviços, inversão de dependência, lifetimes e testes com implementações substitutas.

Conteúdo demonstrado (resumo):

- Registro de serviços no contêiner nativo do .NET.
- Lifetimes `Singleton`, `Scoped` e `Transient`.
- Dependência de abstrações em vez de implementações concretas.
- Composition Root e configuração centralizada.
- Decorators e opções de substituição para testes.
- Validação de dependências e prevenção de Service Locator.

## Estrutura

- A implementação do exemplo deve ficar em `src/DependencyInjection.Showcase`.
- Os testes devem ficar em `tests/DependencyInjection.Showcase.Tests`.

## Como usar

```powershell
dotnet build
dotnet run
dotnet test
```

## Decisões de engenharia

A composição deve permanecer na borda da aplicação, enquanto os serviços recebem contratos por construtor. Lifetimes são escolhidos conforme o ciclo de vida do estado e não apenas por conveniência.
