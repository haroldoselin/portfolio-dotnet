using DependencyInjection.Showcase.Application.Contracts;
using DependencyInjection.Showcase.Domain;

namespace DependencyInjection.Showcase.Application.Services;

public sealed class OrderProcessor(IOrderRepository repository)
{
    public Order Process(int orderId)
    {
        var order = repository.GetById(orderId)
            ?? throw new KeyNotFoundException($"Pedido '{orderId}' não encontrado.");

        return order with { Status = "Processado" };
    }
}