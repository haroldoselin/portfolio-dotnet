using CadastroCliente.Domain.ValueObjects;

namespace CadastroCliente.Application.UseCases.CadastrarCliente;

public sealed class CadastrarClienteValidator
{
    public string? Validate(CadastrarClienteCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Nome))
        {
            return "Nome é obrigatório.";
        }

        return Cpf.TryCreate(command.Cpf, out _) ? null : "CPF inválido.";
    }
}
