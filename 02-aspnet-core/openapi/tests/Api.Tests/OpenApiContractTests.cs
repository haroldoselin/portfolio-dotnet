using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace OpenApi.ShowCase.Api.Tests;

public sealed class OpenApiContractTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public OpenApiContractTests(WebApplicationFactory<Program> factory) => _client = factory.CreateClient();

    [Fact]
    public async Task OpenApi_document_is_available()
    {
        var response = await _client.GetAsync("/openapi/v1.json");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("OpenApi.ShowCase", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Catalog_returns_products()
    {
        var response = await _client.GetAsync("/api/v1/catalogo/");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("API de pagamentos", await response.Content.ReadAsStringAsync());
    }
}
