using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.ValueObjects;
using CadastroCliente.Infrastructure.Persistence.Repositories;
using Xunit;

namespace CadastroCliente.Infrastructure.Tests;

public sealed class ClienteRepositoryTests
{
    [Fact]
    public async Task Deve_adicionar_e_obter_cliente_por_cpf()
    {
        var repository = new ClienteRepository();
        var cliente = CriarCliente("Maria", "52998224725");

        await repository.AdicionarAsync(cliente);
        var encontrado = await repository.ObterPorCpfAsync(cliente.Cpf.Valor);

        Assert.Same(cliente, encontrado);
    }

    [Fact]
    public async Task Deve_rejeitar_cpf_duplicado()
    {
        var repository = new ClienteRepository();
        var primeiro = CriarCliente("Maria", "52998224725");
        var segundo = CriarCliente("João", "52998224725");

        await repository.AdicionarAsync(primeiro);
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => repository.AdicionarAsync(segundo));

        Assert.Equal("CPF já cadastrado.", exception.Message);
    }

    [Fact]
    public async Task Deve_listar_clientes_por_nome()
    {
        var repository = new ClienteRepository();
        await repository.AdicionarAsync(CriarCliente("Zélia", "52998224725"));
        await repository.AdicionarAsync(CriarCliente("Ana", "11144477735"));

        var clientes = await repository.ListarTodosAsync();

        Assert.Equal(["Ana", "Zélia"], clientes.Select(cliente => cliente.Nome));
    }

    private static Cliente CriarCliente(string nome, string valorCpf)
    {
        Assert.True(Cpf.TryCreate(valorCpf, out var cpf));
        return Cliente.Criar(nome, cpf!);
    }
}
