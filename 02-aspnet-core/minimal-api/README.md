# MinimalApi.ShowCase

Minimal API REST em .NET 10 com C# 14 e Clean Architecture.

```text
src/
  MinimalApi.ShowCase.Domain          entidades e interfaces
  MinimalApi.ShowCase.Application     casos de uso e DTOs
  MinimalApi.ShowCase.Infrastructure  persistência em memória e DI
  MinimalApi.ShowCase.Api             endpoints Minimal API e Swagger
tests/
  MinimalApi.ShowCase.Tests           testes unitários
```

Executar:

```bash
dotnet run --project src/MinimalApi.ShowCase.Api
dotnet test MinimalApi.sln
```

O Swagger abre na raiz `/`. Os endpoints são `GET /api/v1/produtos/` e `POST /api/v1/produtos/`.

