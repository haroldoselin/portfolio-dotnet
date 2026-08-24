using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.ValueObjects;
using Xunit;

namespace CadastroCliente.Domain.Tests;

public sealed class ClienteTests
{
    [Fact]
    public void Cria_cliente_com_cpf_valido()
    {
        Assert.True(Cpf.TryCreate("52998224725", out var cpf));
        var cliente = Cliente.Criar("Maria", cpf!);

        Assert.Equal("Maria", cliente.Nome);
    }

    [Fact]
    public void Rejeita_cpf_invalido()
    {
        Assert.False(Cpf.TryCreate("123", out _));
    }
}
