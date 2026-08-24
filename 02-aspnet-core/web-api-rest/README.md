# WebApi.ShowCase — Cadastro de Clientes

Subpasta: `02-aspnet-core/web-api-rest`

Objetivo: demonstrar uma Web API REST em .NET 10 orientada a um cenário de cadastro de clientes, com Controllers, Clean Architecture, validação de domínio, persistência em memória, Swagger e testes automatizados.

## O que este projeto demonstra

- ASP.NET Core Web API com .NET 10, C# 14, nullable habilitado e warnings tratados como erros.
- Controllers com `GET /api/clientes` e `POST /api/clientes`.
- Separação entre Domain, Application, Infrastructure e Api.
- Entidade `Cliente` e value object imutável `Cpf` com validação de formato e invariantes.
- Caso de uso `CadastrarCliente` com command, handler, validação e DTO.
- Interface `IClienteRepository` no domínio, sem acoplamento a banco de dados.
- Persistência singleton em memória baseada em `ConcurrentDictionary`.
- Swagger UI configurado como página inicial da aplicação.
- Testes unitários por camada e testes do comportamento dos controllers.

## Estrutura

```text
WebApi.ShowCase.sln
src/
  Domain/                              entidades, value objects e interfaces
  Application/                         casos de uso, comandos, validação e DTOs
  Infrastructure/                      persistência em memória e composição de DI
  Api/                                 Controllers, pipeline e Swagger
tests/
  Domain.Tests/                        regras de domínio
  Application.Tests/                   handlers e casos de uso
  Infrastructure.Tests/                repositório em memória
  Api.Tests/                           comportamento dos controllers
```

## Como executar

```powershell
dotnet build WebApi.ShowCase.sln
dotnet run --project src/Api/WebApi.ShowCase.Api.csproj
dotnet test WebApi.ShowCase.sln
```

Com a aplicação em execução:

- Swagger UI: `http://localhost:<porta>/`
- Documento OpenAPI: `http://localhost:<porta>/swagger/v1/swagger.json`
- Listagem: `GET /api/clientes`
- Cadastro: `POST /api/clientes`

Exemplo de cadastro:

```json
{
  "nome": "Maria Silva",
  "cpf": "52998224725"
}
```

## Decisões de engenharia

O domínio concentra as regras de `Cliente` e `Cpf`, enquanto a Application coordena o fluxo de cadastro por meio de ports. A API é responsável pelo transporte HTTP e pela exposição da documentação; a infraestrutura fornece uma implementação volátil do repositório, suficiente para executar o showcase sem dependências externas.

O CPF é normalizado para apenas dígitos antes de ser armazenado. A consulta por CPF usa o índice do `ConcurrentDictionary`, oferecendo lookup direto e segurança para acesso concorrente. A listagem retorna um snapshot ordenado, evitando expor a coleção interna.

Os testes estão distribuídos por responsabilidade: domínio e invariantes, aplicação e casos de uso, persistência em memória e comportamento dos controllers. Essa divisão facilita manutenção, diagnóstico de falhas e evolução para uma persistência real sem alterar o contrato de aplicação.
