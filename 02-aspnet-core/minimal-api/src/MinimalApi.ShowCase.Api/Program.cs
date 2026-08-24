using Microsoft.OpenApi.Models;
using MinimalApi.ShowCase.Application.DTOs;
using MinimalApi.ShowCase.Application.UseCases.CadastrarProduto;
using MinimalApi.ShowCase.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();
builder.Services.AddScoped<CadastrarProdutoHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MinimalApi.ShowCase",
        Version = "v1",
        Description = "Minimal API REST demonstrando Clean Architecture em .NET 10."
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "MinimalApi.ShowCase v1");
    options.RoutePrefix = string.Empty;
});

var produtos = app.MapGroup("/api/v1/produtos")
    .WithTags("Produtos");

produtos.MapGet("/", async (CadastrarProdutoHandler handler, CancellationToken cancellationToken) =>
    Results.Ok(await handler.ListarTodosAsync(cancellationToken)))
    .WithName("ListarProdutos")
    .WithSummary("Lista todos os produtos")
    .Produces<IReadOnlyCollection<ProdutoResponseDto>>(StatusCodes.Status200OK);

produtos.MapPost("/", async (
        CadastrarProdutoRequest request,
        CadastrarProdutoHandler handler,
        CancellationToken cancellationToken) =>
    {
        var result = await handler.HandleAsync(
            new CadastrarProdutoCommand(request.Nome, request.Preco),
            cancellationToken);

        return result.Response is { } response
            ? Results.Created($"/api/v1/produtos/{response.Id}", response)
            : Results.BadRequest(new { error = result.Error });
    })
    .WithName("CadastrarProduto")
    .WithSummary("Cadastra um produto")
    .Produces(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest);

app.Run();

public sealed record CadastrarProdutoRequest(string Nome, decimal Preco);


