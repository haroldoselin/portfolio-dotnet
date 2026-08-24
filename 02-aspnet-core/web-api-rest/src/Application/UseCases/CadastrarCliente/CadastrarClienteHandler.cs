using CadastroCliente.Application.DTOs;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.ValueObjects;

namespace CadastroCliente.Application.UseCases.CadastrarCliente;

public sealed class CadastrarClienteHandler(IClienteRepository repository, CadastrarClienteValidator validator)
{
    public async Task<IReadOnlyCollection<ClienteResponseDto>> ListarTodosAsync(
        CancellationToken cancellationToken = default)
    {
        var clientes = await repository.ListarTodosAsync(cancellationToken);

        return clientes
            .Select(cliente => new ClienteResponseDto(cliente.Id, cliente.Nome, cliente.Cpf.Valor))
            .ToArray();
    }

    public async Task<(ClienteResponseDto? Response, string? Error)> HandleAsync(CadastrarClienteCommand command, CancellationToken cancellationToken = default)
    {
        if (validator.Validate(command) is { } error)
        {
            return (null, error);
        }

        Cpf.TryCreate(command.Cpf, out var cpf);

        if (await repository.ObterPorCpfAsync(cpf!.Valor, cancellationToken) is not null)
        {
            return (null, "CPF já cadastrado.");
        }

        var cliente = Cliente.Criar(command.Nome, cpf);
        await repository.AdicionarAsync(cliente, cancellationToken);

        return (new ClienteResponseDto(cliente.Id, cliente.Nome, cliente.Cpf.Valor), null);
    }
}
