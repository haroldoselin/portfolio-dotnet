using CadastroCliente.Domain.Entities;

namespace CadastroCliente.Domain.Interfaces;

public interface IClienteRepository
{
    Task<IReadOnlyCollection<Cliente>> ListarTodosAsync(
        CancellationToken cancellationToken = default);

    Task<Cliente?> ObterPorCpfAsync(
        string cpf,
        CancellationToken cancellationToken = default);

    Task AdicionarAsync(
        Cliente cliente,
        CancellationToken cancellationToken = default);
}
