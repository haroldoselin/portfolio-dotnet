# MinimalApi.ShowCase — ASP.NET Core Minimal API

Subpasta: `02-aspnet-core/minimal-api`

Objetivo: demonstrar uma Minimal API REST em .NET 10, construída para ser pequena, executável e testável, aplicando Clean Architecture, separação de responsabilidades e documentação OpenAPI.

## O que este projeto demonstra

- ASP.NET Core Minimal API com .NET 10, C# 14, nullable habilitado e warnings tratados como erros.
- Endpoints HTTP para cadastro e listagem de produtos em `/api/v1/produtos`.
- Separação entre Domain, Application, Infrastructure e Api.
- Entidade de domínio `Produto` com invariantes de nome e preço.
- Caso de uso `CadastrarProduto` com handler, command e DTO de resposta.
- Port `IProdutoRepository` no domínio e persistência volátil em `ConcurrentDictionary`.
- Injeção de dependência por extensão de infraestrutura.
- Swagger UI disponível na raiz da aplicação e documento OpenAPI versionado.
- Testes unitários da entidade, handler e testes de contrato HTTP da API.

## Estrutura

```text
MinimalApi.sln
src/
  MinimalApi.ShowCase.Domain             entidades e ports do domínio
  MinimalApi.ShowCase.Application        commands, handlers e DTOs
  MinimalApi.ShowCase.Infrastructure     repositório em memória e composição
  MinimalApi.ShowCase.Api                Minimal API, endpoints e Swagger
tests/
  MinimalApi.ShowCase.Tests              domínio e infraestrutura
  MinimalApi.ShowCase.Application.Tests  testes unitários da aplicação
  MinimalApi.ShowCase.Api.Tests          testes HTTP da API e Swagger
```

## Como executar

```powershell
dotnet build MinimalApi.sln
dotnet run --project src/MinimalApi.ShowCase.Api
dotnet test MinimalApi.sln
```

Com a aplicação em execução:

- Swagger UI: `http://localhost:<porta>/`
- Documento OpenAPI: `http://localhost:<porta>/swagger/v1/swagger.json`
- Listagem: `GET /api/v1/produtos/`
- Cadastro: `POST /api/v1/produtos/`

Exemplo de payload:

```json
{
  "nome": "Livro de arquitetura",
  "preco": 49.90
}
```

## Decisões de engenharia

O domínio não conhece ASP.NET Core nem a implementação do repositório. A aplicação coordena o caso de uso por meio de um port, enquanto a API permanece responsável apenas pelo transporte HTTP e pelos metadados OpenAPI. A persistência em memória mantém o showcase independente de banco externo e evidencia o contrato de infraestrutura; por ser volátil, os dados são perdidos ao reiniciar a aplicação.

Os testes da aplicação usam um repositório falso para validar regras determinísticas sem infraestrutura. Os testes da API usam `WebApplicationFactory` para verificar o comportamento real do pipeline, incluindo status HTTP, serialização JSON e disponibilidade do Swagger.
