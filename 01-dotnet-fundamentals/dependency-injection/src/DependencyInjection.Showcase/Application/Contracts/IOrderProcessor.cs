using DependencyInjection.Showcase.Domain;

namespace DependencyInjection.Showcase.Application.Contracts;

public interface IOrderProcessor
{
    Order Process(int orderId);
}