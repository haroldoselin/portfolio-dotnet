using DependencyInjection.Showcase.Domain;

namespace DependencyInjection.Showcase.Application.Contracts;

public interface IOrderRepository
{
    Order? GetById(int id);
}