# .NET Senior Portfolio

Portfólio técnico focado no ecossistema Microsoft .NET,
arquitetura de software, backend, cloud e engenharia de sistemas.

## Stack

.NET 10
C# 14
ASP.NET Core
Entity Framework Core
Dapper
SQL Server
Redis
RabbitMQ
Docker
Azure
GitHub Actions
OpenTelemetry

## Arquitetura

Clean Architecture
Domain-Driven Design
CQRS
Vertical Slice Architecture
SOLID
Design Patterns

## Engenharia

Testes automatizados
Performance
Observabilidade
Segurança
CI/CD
Resiliência
Cloud

## Projetos

01 - C# / .NET Fundamentals

- Subpasta: /csharp-14
- Objetivo: consolidar os fundamentos da linguagem C# 14 e do runtime .NET 10, com exemplos práticos e exercícios que demonstram padrões, boas práticas e recursos de linguagem.

Conteúdo demonstrado (resumo):
- Estrutura de um projeto .NET (csproj, pastas, convenções) e uso de top-level statements.
- Sintaxe e recursos do C# 14: records, pattern matching, switch expressions e aprimoramentos de linguagem.
- Tipos e segurança de null: referenciais anuláveis (nullable reference types) e proteção contra NREs.
- Programação assíncrona: async/await, Task, e padrões de I/O assíncrono.
- Delegates, eventos, expressões lambda, métodos locais e closures.
- Generics, coleções imutáveis/imutabilidade e uso de LINQ para consultas e transformações.
- Manipulação de exceções, logging e princípios de design para tratamento de erros.
- Noções de desempenho: Span<T>, Memory<T>, alocação e práticas para reduzir GC pressure.
- Testes básicos: estrutura de testes unitários e como executar testes (dotnet test) quando presentes.
- Exemplos práticos: utilitários, pequenos CLI, algoritmos e modelos de domínio simples para ilustrar conceitos.

Como usar (rápido):
- Abra a pasta: /csharp-14
- Build: dotnet build
- Run: dotnet run (na pasta do projeto desejado)
- Tests: dotnet test (se houver projetos de teste)

Portfolio .NET 10 (visão geral)

- Subpasta: /dotnet-10.
- Objetivo: demonstrar capacidade prática com o ecossistema .NET 10 — construção de APIs, acesso a dados, mensageria, contêineres, deploy em cloud, observabilidade e automação de CI/CD.

Conteúdo demonstrado (resumo):
- Plataforma e tooling: .NET 10 SDK, dotnet CLI, integração com Visual Studio 2022/2026, e arquivos de solução (.sln) e projetos (.csproj).
- Web e APIs: ASP.NET Core minimal APIs e controllers, middleware, autenticação/autorização básica e boas práticas para rotas e versionamento.
- Data access: Entity Framework Core (migrations, DbContext, patterns), Dapper para queries performáticas e estratégias de mapeamento.
- Bancos e cache: SQL Server para persistência relacional e Redis para cache/distribuição de sessão.
- Mensageria: RabbitMQ para integração assíncrona e padrões de pub/sub e filas.
- Contêinerização e infra: Dockerfiles, docker-compose para orquestração local e orientações para criação de imagens leves.
- Cloud e CI/CD: exemplos de deploy para Azure (App Service / Container Registry) e pipelines de automação via GitHub Actions.
- Observabilidade: OpenTelemetry para traces e métricas, integração com logs estruturados e exportadores.
- Segurança: princípios de segurança em APIs, proteção de segredos, e práticas de hardening básicas.
- Arquitetura: demonstração de Clean Architecture, DDDLight, CQRS e Vertical Slice em pequenos projetos referenciais.
- Testes e qualidade: unidades, testes de integração quando aplicáveis, e configuração básica para execução de testes automatizados.
- Performance: exemplos de benchmarking, uso de Span/Memory, pool de arrays e práticas para minimizar alocações desnecessárias.

Como usar (rápido, visão do portfólio):
- Abra a solução principal ou a pasta do exemplo desejado na raiz do repositório
- Build: dotnet build (ou usar Build no Visual Studio)
- Run: dotnet run (ou executar via Visual Studio / Docker conforme o projeto)
- Docker: docker-compose up --build (quando aplicável)
- Tests: dotnet test (na solução ou em projetos de teste específicos)
- CI: ver arquivo .github/workflows para pipelines de integração e deploy

02 - ASP.NET Core
03 - Data Access
04 - Architecture
05 - Security
06 - Testing
07 - Performance
08 - Messaging
09 - Docker
10 - Azure
11 - Observability
12 - Enterprise Project
