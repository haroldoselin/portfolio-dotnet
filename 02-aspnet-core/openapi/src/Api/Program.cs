using Microsoft.AspNetCore.Mvc;
using OpenApi.ShowCase.Api.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info.Title = "OpenApi.ShowCase";
        document.Info.Version = "v1";
        document.Info.Description = "Catálogo demonstrando OpenAPI nativo no ASP.NET Core 10.";
        return Task.CompletedTask;
    });
});

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference(options => options.WithTitle("OpenApi.ShowCase | API Reference"));

var catalogo = app.MapGroup("/api/v1/catalogo").WithTags("Catálogo");
catalogo.MapGet("/", () => Results.Ok(new[]
{
    new Produto("api-001", "API de pagamentos", "Ativa"),
    new Produto("api-002", "API de identidade", "Beta")
}))
.WithName("ListarCatalogo")
.WithSummary("Lista os produtos de API disponíveis")
.Produces<IReadOnlyCollection<Produto>>(StatusCodes.Status200OK);

catalogo.MapGet("/{id}", (string id) =>
{
    var produto = new Produto("api-001", "API de pagamentos", "Ativa");
    return produto.Id.Equals(id, StringComparison.OrdinalIgnoreCase)
        ? Results.Ok(produto)
        : Results.NotFound(new ProblemDetails { Title = "Produto não encontrado" });
})
.WithName("ObterProduto")
.WithSummary("Obtém um produto de API pelo identificador")
.Produces<Produto>(StatusCodes.Status200OK)
.Produces<ProblemDetails>(StatusCodes.Status404NotFound);

app.Run();

public partial class Program;

namespace OpenApi.ShowCase.Api.Models
{
    public sealed record Produto(string Id, string Nome, string Status);
}
