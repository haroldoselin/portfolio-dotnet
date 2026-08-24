using MinimalApi.ShowCase.Application.UseCases.CadastrarProduto;
using MinimalApi.ShowCase.Domain.Entities;
using MinimalApi.ShowCase.Domain.Interfaces;
using Xunit;

namespace MinimalApi.ShowCase.Application.Tests;

public sealed class CadastrarProdutoHandlerTests
{
    [Fact]
    public async Task Deve_cadastrar_produto_valido()
    {
        var repository = new FakeProdutoRepository();
        var handler = new CadastrarProdutoHandler(repository);

        var result = await handler.HandleAsync(new CadastrarProdutoCommand("Livro", 49.90m));

        Assert.NotNull(result.Response);
        Assert.Null(result.Error);
        Assert.Equal("Livro", result.Response!.Nome);
        Assert.Equal(49.90m, result.Response.Preco);
        Assert.Single(repository.Produtos);
    }

    [Theory]
    [InlineData("", 10, "Nome é obrigatório.")]
    [InlineData("Livro", 0, "Preço deve ser maior que zero.")]
    [InlineData("Livro", -1, "Preço deve ser maior que zero.")]
    public async Task Deve_rejeitar_comando_invalido(string nome, decimal preco, string mensagem)
    {
        var repository = new FakeProdutoRepository();
        var handler = new CadastrarProdutoHandler(repository);

        var result = await handler.HandleAsync(new CadastrarProdutoCommand(nome, preco));

        Assert.Null(result.Response);
        Assert.Equal(mensagem, result.Error);
        Assert.Empty(repository.Produtos);
    }

    [Fact]
    public async Task Deve_listar_produtos_mapeados_e_ordenados()
    {
        var repository = new FakeProdutoRepository();
        var handler = new CadastrarProdutoHandler(repository);
        await handler.HandleAsync(new CadastrarProdutoCommand("Zebra", 10));
        await handler.HandleAsync(new CadastrarProdutoCommand("Abacate", 5));

        var result = await handler.ListarTodosAsync();

        Assert.Equal(["Abacate", "Zebra"], result.Select(produto => produto.Nome));
    }

    private sealed class FakeProdutoRepository : IProdutoRepository
    {
        private readonly List<Produto> produtos = [];
        public IReadOnlyCollection<Produto> Produtos => produtos;

        public Task<IReadOnlyCollection<Produto>> ListarTodosAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyCollection<Produto>>(produtos.OrderBy(produto => produto.Nome).ToArray());

        public Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default)
        {
            produtos.Add(produto);
            return Task.CompletedTask;
        }
    }
}
