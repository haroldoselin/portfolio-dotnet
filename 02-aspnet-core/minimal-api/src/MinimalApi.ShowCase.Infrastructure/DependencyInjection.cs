using Microsoft.Extensions.DependencyInjection;
using MinimalApi.ShowCase.Domain.Interfaces;
using MinimalApi.ShowCase.Infrastructure.Persistence;

namespace MinimalApi.ShowCase.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services.AddSingleton<IProdutoRepository, InMemoryProdutoRepository>();
    }
}
