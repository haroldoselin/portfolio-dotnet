using CadastroCliente.Api.Controllers;
using CadastroCliente.Application.DTOs;
using CadastroCliente.Application.UseCases.CadastrarCliente;
using CadastroCliente.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CadastroCliente.Api.Tests;

public sealed class ClientesControllerTests
{
    [Fact]
    public async Task GetAll_deve_retornar_ok_com_clientes()
    {
        var handler = new CadastrarClienteHandler(new ClienteRepository(), new CadastrarClienteValidator());
        await handler.HandleAsync(new CadastrarClienteCommand("Maria", "52998224725"));
        var controller = new ClientesController(handler);

        var result = await controller.GetAll(CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result);
        var clientes = Assert.IsAssignableFrom<IReadOnlyCollection<ClienteResponseDto>>(ok.Value);
        Assert.Single(clientes);
    }

    [Fact]
    public async Task Post_deve_retornar_created_para_cliente_valido()
    {
        var controller = new ClientesController(new CadastrarClienteHandler(new ClienteRepository(), new CadastrarClienteValidator()));

        var result = await controller.Post(new CadastrarClienteCommand("Maria", "52998224725"), CancellationToken.None);

        Assert.IsType<CreatedResult>(result);
    }
}
