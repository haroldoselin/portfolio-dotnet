using DotNet10.Showcase.Domain.Entities;
using DotNet10.Showcase.Domain.Enuns;
using DotNet10.Showcase.Domain.ValueObjects;
using DotNet10.Showcase.Application.Contracts;
using System.Collections.Concurrent;

namespace DotNet10.Showcase.Infrastructure.Persistence
{
    public sealed class InMemoryOrderRepository : IOrderRepository
    {
        private readonly ConcurrentDictionary<Guid, Order> _orders = new();

        public Task AddAsync(
            Order order,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!_orders.TryAdd(order.Id.Value, order))
            {
                throw new InvalidOperationException(
                    $"Order '{order.Id}' already exists.");
            }

            return Task.CompletedTask;
        }

        public Task<Order?> GetByIdAsync(
            OrderId orderId,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _orders.TryGetValue(
                orderId.Value,
                out var order);

            return Task.FromResult(order);
        }

        public Task<IReadOnlyCollection<Order>> GetPendingAsync(
            int batchSize,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var orders = _orders.Values
                .Where(order => order.Status == OrderStatus.Confirmed)
                .Take(batchSize)
                .ToArray();

            return Task.FromResult<IReadOnlyCollection<Order>>(orders);
        }

        public Task SaveChangesAsync(
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.CompletedTask;
        }
    }
}
