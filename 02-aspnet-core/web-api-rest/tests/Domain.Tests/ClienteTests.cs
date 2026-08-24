using CadastroCliente.Domain.Entities;
using CadastroCliente.Domain.ValueObjects;
using Xunit;

namespace CadastroCliente.Domain.Tests;

public sealed class ClienteTests
{
    [Fact]
    public void Deve_criar_cliente_com_cpf_valido()
    {
        Assert.True(Cpf.TryCreate("52998224725", out var cpf));

        var cliente = Cliente.Criar("Maria Silva", cpf!);

        Assert.NotEqual(Guid.Empty, cliente.Id);
        Assert.Equal("Maria Silva", cliente.Nome);
        Assert.Equal("52998224725", cliente.Cpf.Valor);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("11111111111")]
    [InlineData(null)]
    public void Deve_rejeitar_cpf_invalido(string? valor)
    {
        Assert.False(Cpf.TryCreate(valor, out _));
    }

    [Fact]
    public void Deve_rejeitar_nome_vazio()
    {
        Assert.True(Cpf.TryCreate("52998224725", out var cpf));

        Assert.Throws<ArgumentException>(() => Cliente.Criar(" ", cpf!));
    }
}
