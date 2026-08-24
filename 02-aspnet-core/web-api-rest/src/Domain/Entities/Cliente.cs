namespace CadastroCliente.Domain.Entities;

using CadastroCliente.Domain.ValueObjects;

public sealed class Cliente
{
    private Cliente(Guid id, string nome, Cpf cpf) => (Id, Nome, Cpf) = (id, nome, cpf);
    public Guid Id { get; }
    public string Nome { get; }
    public Cpf Cpf { get; }

    public static Cliente Criar(string nome, Cpf cpf)
    {
        return string.IsNullOrWhiteSpace(nome)
            ? throw new ArgumentException("Nome é obrigatório.", nameof(nome))
            : new Cliente(Guid.NewGuid(), nome.Trim(), cpf);
    }
}
