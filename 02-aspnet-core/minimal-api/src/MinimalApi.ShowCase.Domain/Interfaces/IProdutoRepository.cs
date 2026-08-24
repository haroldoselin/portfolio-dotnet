using MinimalApi.ShowCase.Domain.Entities;

namespace MinimalApi.ShowCase.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<IReadOnlyCollection<Produto>> ListarTodosAsync(CancellationToken cancellationToken = default);
    Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default);
}
