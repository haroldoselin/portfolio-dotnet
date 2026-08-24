using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using MinimalApi.ShowCase.Application.DTOs;
using Xunit;

namespace MinimalApi.ShowCase.Api.Tests;

public sealed class ProdutosApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public ProdutosApiTests(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_produtos_deve_retornar_ok_com_lista()
    {
        var response = await client.GetAsync("/api/v1/produtos/");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var produtos = await response.Content.ReadFromJsonAsync<ProdutoResponseDto[]>();
        Assert.NotNull(produtos);
    }

    [Fact]
    public async Task Post_produto_deve_retornar_created()
    {
        var response = await client.PostAsJsonAsync(
            "/api/v1/produtos/",
            new { Nome = "Livro", Preco = 49.90m });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(await response.Content.ReadFromJsonAsync<ProdutoResponseDto>());
        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task Post_produto_invalido_deve_retornar_bad_request()
    {
        var response = await client.PostAsJsonAsync(
            "/api/v1/produtos/",
            new { Nome = "", Preco = 0m });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Raiz_deve_exibir_swagger_ui()
    {
        var response = await client.GetAsync("/");
        var content = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("swagger-ui", content, StringComparison.OrdinalIgnoreCase);
    }
}
