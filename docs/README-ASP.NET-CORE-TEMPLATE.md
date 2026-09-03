# [Nome do projeto] — ASP.NET Core

Subpasta: `02-aspnet-core/[nome-do-projeto]`

Objetivo: [descrever o problema técnico e o objetivo do showcase].

## O que este projeto demonstra

- .NET 10, C# 14, nullable habilitado e warnings tratados como erros.
- ASP.NET Core [Minimal API/Web API/MVC].
- [Padrão arquitetural ou abordagem principal].
- [Domínio, entidades, value objects e regras relevantes].
- [Casos de uso, handlers, commands, queries ou serviços].
- [Persistência, integrações ou infraestrutura].
- Swagger/OpenAPI em [rota].
- Testes automatizados de [camadas e cenários].
- [Benchmark, observabilidade, segurança ou resiliência, quando aplicável].

## Estrutura

```text
[nome-da-solução].sln
src/
  [Projeto.Domain]               [responsabilidade]
  [Projeto.Application]          [responsabilidade]
  [Projeto.Infrastructure]       [responsabilidade]
  [Projeto.Api]                  [responsabilidade]
tests/
  [Projeto.Domain.Tests]         [cobertura]
  [Projeto.Application.Tests]    [cobertura]
  [Projeto.Infrastructure.Tests] [cobertura]
  [Projeto.Api.Tests]            [cobertura]
```

## Como executar

```powershell
dotnet restore [nome-da-solução].sln
dotnet build [nome-da-solução].sln
dotnet run --project src/[Projeto.Api]/[Projeto.Api].csproj
dotnet test [nome-da-solução].sln
```

## Endpoints

| Método | Rota | Resultado |
| --- | --- | --- |
| GET | `/api/v1/[recurso]` | [resultado] |
| POST | `/api/v1/[recurso]` | [resultado] |

Swagger UI: `http://localhost:<porta>/`

OpenAPI JSON: `http://localhost:<porta>/swagger/v1/swagger.json`

## Exemplo de requisição

```json
{
  "[campo]": "[valor]"
}
```

## Decisões de engenharia

[Explicar as principais decisões, limites do showcase, separação de responsabilidades, persistência, concorrência, validação e possibilidades de evolução.]

## Estratégia de testes

[Descrever testes de domínio, aplicação, infraestrutura e API, incluindo o que é unitário e o que valida o pipeline HTTP.]
