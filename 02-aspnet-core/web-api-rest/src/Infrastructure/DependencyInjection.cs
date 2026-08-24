using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CadastroCliente.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services.AddSingleton<IClienteRepository, ClienteRepository>();
    }
}
