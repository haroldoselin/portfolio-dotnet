using System.Collections.Concurrent;
using MinimalApi.ShowCase.Domain.Entities;
using MinimalApi.ShowCase.Domain.Interfaces;

namespace MinimalApi.ShowCase.Infrastructure.Persistence;

public sealed class InMemoryProdutoRepository : IProdutoRepository
{
    private readonly ConcurrentDictionary<Guid, Produto> produtos = new();

    public Task<IReadOnlyCollection<Produto>> ListarTodosAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<Produto> resultado = produtos.Values
            .OrderBy(produto => produto.Nome)
            .ToArray();

        return Task.FromResult(resultado);
    }

    public Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default)
    {
        produtos.TryAdd(produto.Id, produto);
        return Task.CompletedTask;
    }
}
