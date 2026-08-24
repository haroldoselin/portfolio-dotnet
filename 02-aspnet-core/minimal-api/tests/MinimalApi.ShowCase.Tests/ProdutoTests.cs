using MinimalApi.ShowCase.Application.UseCases.CadastrarProduto;
using MinimalApi.ShowCase.Domain.Entities;
using MinimalApi.ShowCase.Infrastructure.Persistence;
using Xunit;

namespace MinimalApi.ShowCase.Tests;

public sealed class ProdutoTests
{
    [Fact]
    public void Deve_rejeitar_preco_invalido()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Produto.Criar("Livro", 0));
    }

    [Fact]
    public async Task Handler_deve_cadastrar_produto()
    {
        var repository = new InMemoryProdutoRepository();
        var handler = new CadastrarProdutoHandler(repository);

        var result = await handler.HandleAsync(new CadastrarProdutoCommand("Livro", 49.90m));

        Assert.NotNull(result.Response);
        Assert.Null(result.Error);
        Assert.Equal("Livro", result.Response!.Nome);
    }

    [Fact]
    public async Task Handler_deve_listar_produtos_ordenados()
    {
        var repository = new InMemoryProdutoRepository();
        var handler = new CadastrarProdutoHandler(repository);
        await handler.HandleAsync(new CadastrarProdutoCommand("Zebra", 10));
        await handler.HandleAsync(new CadastrarProdutoCommand("Abacate", 5));

        var produtos = await handler.ListarTodosAsync();

        Assert.Equal(["Abacate", "Zebra"], produtos.Select(produto => produto.Nome));
    }
}
