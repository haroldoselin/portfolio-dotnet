# Dependency Injection — C# / .NET Fundamentals

Subpasta: `01-dotnet-fundamentals/dependency-injection`

Objetivo: demonstrar Dependency Injection no .NET 10 por meio de um processador de pedidos, aplicando inversão de dependência, composição centralizada, lifetimes e substituição de implementações em testes.

Conteúdo demonstrado (resumo):

- Registro de serviços no contêiner nativo do .NET.
- Lifetimes `Singleton`, `Scoped` e `Transient`.
- Injeção de abstrações por construtor.
- Composition Root centralizado no `Program.cs`.
- Decorator de logging sem modificar o serviço de negócio.
- `ServiceProviderOptions` com `ValidateScopes` e `ValidateOnBuild`.
- Implementações fake para testes unitários.
- Benchmark do processamento com o grafo de dependências composto.
- Prevenção de Service Locator: a aplicação resolve apenas o contrato na borda.

## Estrutura

- `src/DependencyInjection.Showcase`: domínio, contratos, serviços, infraestrutura e Composition Root.
- `src/DependencyInjection.Showcase.Benchmarks`: benchmark do processamento de pedidos.
- `tests/DependencyInjection.Showcase.Tests`: testes automatizados com xUnit.

## Como usar

```powershell
dotnet build DependencyInjection.Showcase.slnx
dotnet run --project src/DependencyInjection.Showcase
dotnet test DependencyInjection.Showcase.slnx
dotnet run --project src/DependencyInjection.Showcase.Benchmarks -c Release
```

## Decisões de engenharia

O repositório é Singleton porque não mantém estado mutável por requisição, o contexto de operação é Scoped e o processador é Transient. O decorator é registrado para o contrato `IOrderProcessor`, mantendo o serviço de aplicação independente de logging e demonstrando extensão de comportamento sem alterar a regra de negócio.

A composição permanece na borda da aplicação e o `ServiceProvider` é validado na inicialização. Nos testes, o serviço pode receber um repositório fake diretamente, preservando isolamento e evitando dependência de um container para testar regras de negócio.
