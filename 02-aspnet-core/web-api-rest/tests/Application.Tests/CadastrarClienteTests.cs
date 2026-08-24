using CadastroCliente.Application.UseCases.CadastrarCliente;
using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.ValueObjects;
using Xunit;

namespace CadastroCliente.Application.Tests;

public sealed class CadastrarClienteTests
{
    [Fact]
    public async Task Deve_cadastrar_cliente_com_dados_validos()
    {
        var repository = new FakeClienteRepository();
        var handler = new CadastrarClienteHandler(repository, new CadastrarClienteValidator());

        var result = await handler.HandleAsync(new CadastrarClienteCommand("Maria Silva", "52998224725"));

        Assert.NotNull(result.Response);
        Assert.Null(result.Error);
        Assert.Equal("Maria Silva", result.Response!.Nome);
        Assert.Single(repository.Clientes);
    }

    [Fact]
    public async Task Deve_rejeitar_cpf_duplicado()
    {
        var repository = new FakeClienteRepository();
        var handler = new CadastrarClienteHandler(repository, new CadastrarClienteValidator());
        var command = new CadastrarClienteCommand("Maria", "52998224725");

        await handler.HandleAsync(command);
        var result = await handler.HandleAsync(command);

        Assert.Null(result.Response);
        Assert.Equal("CPF já cadastrado.", result.Error);
    }

    [Fact]
    public async Task Deve_listar_clientes_ordenados_por_nome()
    {
        var repository = new FakeClienteRepository();
        var handler = new CadastrarClienteHandler(repository, new CadastrarClienteValidator());
        await handler.HandleAsync(new CadastrarClienteCommand("Zélia", "52998224725"));
        await handler.HandleAsync(new CadastrarClienteCommand("Ana", "11144477735"));

        var clientes = await handler.ListarTodosAsync();

        Assert.Equal(["Ana", "Zélia"], clientes.Select(cliente => cliente.Nome));
    }

    private sealed class FakeClienteRepository : IClienteRepository
    {
        private readonly List<Cliente> clientes = [];
        public IReadOnlyCollection<Cliente> Clientes => clientes;
        public Task<IReadOnlyCollection<Cliente>> ListarTodosAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyCollection<Cliente>>(clientes.OrderBy(cliente => cliente.Nome).ToArray());
        public Task<Cliente?> ObterPorCpfAsync(string cpf, CancellationToken cancellationToken = default) =>
            Task.FromResult(clientes.SingleOrDefault(cliente => cliente.Cpf.Valor == cpf));
        public Task AdicionarAsync(Cliente cliente, CancellationToken cancellationToken = default)
        {
            clientes.Add(cliente);
            return Task.CompletedTask;
        }
    }
}
