# OpenApi.ShowCase — OpenAPI moderno no ASP.NET Core

Showcase de documentação de API com `Microsoft.AspNetCore.OpenApi` do .NET 10 e Scalar como API Reference interativa.

## O que este projeto demonstra

- OpenAPI 3.1 gerado pelo ASP.NET Core, sem acoplamento ao Swashbuckle.
- Scalar em `/scalar` consumindo `/openapi/v1.json`.
- Minimal API com contratos explícitos, metadados, exemplos e respostas tipadas.
- Versionamento de rota (`/api/v1/catalogo`) e testes de contrato HTTP.

## Estrutura

```text
OpenApi.ShowCase.sln
src/Api        API, endpoints e modelos
tests/Api.Tests testes de contrato
```

## Como executar

```powershell
dotnet build OpenApi.ShowCase.sln
dotnet run --project src/Api
dotnet test OpenApi.ShowCase.sln
```

- Scalar: `http://localhost:<porta>/scalar`
- Documento OpenAPI: `http://localhost:<porta>/openapi/v1.json`
- Catálogo: `GET /api/v1/catalogo`

Scalar é a camada de visualização; o contrato é gerado pelo pipeline oficial do ASP.NET Core.
