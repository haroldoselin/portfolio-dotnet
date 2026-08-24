using MinimalApi.ShowCase.Application.DTOs;
using MinimalApi.ShowCase.Domain.Entities;
using MinimalApi.ShowCase.Domain.Interfaces;

namespace MinimalApi.ShowCase.Application.UseCases.CadastrarProduto;

public sealed class CadastrarProdutoHandler(IProdutoRepository repository)
{
    public async Task<(ProdutoResponseDto? Response, string? Error)> HandleAsync(
        CadastrarProdutoCommand command,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(command.Nome))
        {
            return (null, "Nome é obrigatório.");
        }

        if (command.Preco <= 0)
        {
            return (null, "Preço deve ser maior que zero.");
        }

        var produto = Produto.Criar(command.Nome, command.Preco);
        await repository.AdicionarAsync(produto, cancellationToken);

        return (ToResponse(produto), null);
    }

    public async Task<IReadOnlyCollection<ProdutoResponseDto>> ListarTodosAsync(
        CancellationToken cancellationToken = default)
    {
        var produtos = await repository.ListarTodosAsync(cancellationToken);
        return produtos.Select(ToResponse).ToArray();
    }

    private static ProdutoResponseDto ToResponse(Produto produto) =>
        new(produto.Id, produto.Nome, produto.Preco);
}
