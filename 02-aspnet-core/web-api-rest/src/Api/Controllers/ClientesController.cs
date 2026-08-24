using CadastroCliente.Application.DTOs;
using CadastroCliente.Application.UseCases.CadastrarCliente;
using Microsoft.AspNetCore.Mvc;
namespace CadastroCliente.Api.Controllers;

[ApiController, Route("api/clientes")]
public sealed class ClientesController(CadastrarClienteHandler handler) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<ClienteResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var clientes = await handler.ListarTodosAsync(cancellationToken);

        return Ok(clientes);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CadastrarClienteCommand command, CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(command, cancellationToken);

        return result.Response is { } response
            ? Created($"api/clientes/{response.Id}", response)
            : BadRequest(new { error = result.Error });
    }
}
