using System.Collections.Concurrent;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Interfaces;

namespace CadastroCliente.Infrastructure.Persistence.Repositories;

public sealed class ClienteRepository : IClienteRepository
{
    private readonly ConcurrentDictionary<string, Cliente> clientes = new(StringComparer.Ordinal);

    public Task<IReadOnlyCollection<Cliente>> ListarTodosAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<Cliente> resultado = clientes.Values
            .OrderBy(cliente => cliente.Nome)
            .ToArray();

        return Task.FromResult(resultado);
    }

    public Task<Cliente?> ObterPorCpfAsync(string cpf, CancellationToken cancellationToken = default)
    {
        clientes.TryGetValue(cpf, out var cliente);
        return Task.FromResult(cliente);
    }

    public Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken = default)
    {
        if (!clientes.TryAdd(cliente.Cpf.Valor, cliente))
        {
            throw new InvalidOperationException("CPF já cadastrado.");
        }

        return Task.CompletedTask;
    }
}
