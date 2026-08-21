using DependencyInjection.Showcase.Application.Contracts;
using DependencyInjection.Showcase.Domain;

namespace DependencyInjection.Showcase.Infrastructure;

public sealed class InMemoryOrderRepository : IOrderRepository
{
    private readonly IReadOnlyDictionary<int, Order> orders = new Dictionary<int, Order>
    {
        [1001] = new(1001, 250m),
        [1002] = new(1002, 480m)
    };

    public Order? GetById(int id) =>
        orders.GetValueOrDefault(id);
}