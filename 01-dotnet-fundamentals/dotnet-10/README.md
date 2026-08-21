# .NET 10 — C# / .NET Fundamentals

Subpasta: `01-dotnet-fundamentals/dotnet-10`

Objetivo: demonstrar a construção de uma aplicação .NET 10 orientada a processamento de pedidos, combinando domínio, serviços, hospedagem, configuração, diagnóstico, eventos e processamento em background.

Conteúdo demonstrado (resumo):

- Host genérico e composição de dependências com `HostApplicationBuilder`.
- Configuração tipada com Options e `appsettings`.
- Entidades, value objects, enums e eventos de domínio.
- Contratos de aplicação e repositório em memória.
- Serviço de pedidos e worker hospedado para processamento assíncrono.
- Relógio do sistema abstraído para facilitar testes determinísticos.
- Diagnósticos com `Activity` e instrumentação básica.
- Testes unitários de domínio, aplicação, infraestrutura, hosting e diagnostics.
- BenchmarkDotNet para processamento de pedidos.

## Estrutura

- `src/DotNet10.Showcase`: aplicação, domínio, aplicação e infraestrutura.
- `src/DotNet10.Showcase.Benchmarks`: benchmarks de processamento.
- `tests/DotNet10.Showcase.Tests`: testes automatizados.

## Como usar

```powershell
dotnet build DotNet10.Showcase.slnx
dotnet run --project src/DotNet10.Showcase
dotnet test DotNet10.Showcase.slnx
dotnet run --project src/DotNet10.Showcase.Benchmarks -c Release
```

## Decisões de engenharia

As abstrações de relógio, repositório e serviços permitem testar regras sem dependência de infraestrutura externa. O worker e o diagnóstico demonstram como uma aplicação .NET 10 pode ser preparada para execução contínua e observabilidade.
