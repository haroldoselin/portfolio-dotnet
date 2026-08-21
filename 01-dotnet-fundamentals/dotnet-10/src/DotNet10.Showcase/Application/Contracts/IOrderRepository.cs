using DotNet10.Showcase.Application.Models;
using DotNet10.Showcase.Domain.Entities;
using DotNet10.Showcase.Domain.ValueObjects;

namespace DotNet10.Showcase.Application.Contracts
{
    public interface IOrderRepository
    {
        Task AddAsync(
            Order order,
            CancellationToken cancellationToken);

        Task<Order?> GetByIdAsync(
            OrderId orderId,
            CancellationToken cancellationToken);

        Task<IReadOnlyCollection<Order>> GetPendingAsync(
            int batchSize,
            CancellationToken cancellationToken);

        Task SaveChangesAsync(
            CancellationToken cancellationToken);
    }
}
