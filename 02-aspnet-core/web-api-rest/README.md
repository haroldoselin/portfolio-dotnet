# CadastroCliente

API REST em .NET 10 organizada em camadas simples: `Domain`, `Application`, `Infrastructure` e `Api`.

```text
src/Domain          entidades, value objects e interfaces
src/Application     casos de uso, comandos, validação e DTOs
src/Infrastructure  persistência em memória e injeção de dependências
src/Api              Controllers e configuração HTTP
tests/Domain.Tests   testes das regras de domínio
```

Executar:

```bash
dotnet run --project src/Api
dotnet test CadastroCliente.sln
```

Endpoints principais:

- `GET /api/clientes` — lista todos os clientes.
- `POST /api/clientes` — cadastra um cliente com `{ "nome": "Maria", "cpf": "52998224725" }`.

Documentação interativa: `/` (Swagger UI).

Documento OpenAPI: `/swagger/v1/swagger.json`.
