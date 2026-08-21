using DotNet10.Showcase.Application.Contracts;
using DotNet10.Showcase.Application.Models;
using DotNet10.Showcase.Domain.Entities;
using DotNet10.Showcase.Domain.ValueObjects;
using DotNet10.Showcase.Infrastructure.Time;

namespace DotNet10.Showcase.Application.Services
{
    public sealed class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ISystemClock _clock;

        public OrderService(
            IOrderRepository repository,
            ISystemClock clock)
        {
            _repository = repository;
            _clock = clock;
        }

        public async Task<OrderResponse> CreateAsync(
            CreateOrderRequest request,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var order = Order.Create(
                request.CustomerId,
                new Money(request.Total),
                _clock.UtcNow);

            await _repository.AddAsync(
                order,
                cancellationToken);

            await _repository.SaveChangesAsync(
                cancellationToken);

            return Map(order);
        }

        public async Task<OrderResponse> ConfirmAsync(
            OrderId orderId,
            CancellationToken cancellationToken)
        {
            var order = await GetRequiredOrderAsync(
                orderId,
                cancellationToken);

            order.Confirm(_clock.UtcNow);

            await _repository.SaveChangesAsync(
                cancellationToken);

            return Map(order);
        }

        public async Task<ProcessingResult> ProcessAsync(
            OrderId orderId,
            CancellationToken cancellationToken)
        {
            var order = await GetRequiredOrderAsync(
                orderId,
                cancellationToken);

            order.StartProcessing(_clock.UtcNow);

            await _repository.SaveChangesAsync(
                cancellationToken);

            return new ProcessingResult(
                order.Id,
                true,
                "Order processing started.");
        }

        public async Task<OrderResponse> CompleteAsync(
            OrderId orderId,
            CancellationToken cancellationToken)
        {
            var order = await GetRequiredOrderAsync(
                orderId,
                cancellationToken);

            order.Complete(_clock.UtcNow);

            await _repository.SaveChangesAsync(
                cancellationToken);

            return Map(order);
        }

        public async Task<OrderResponse> CancelAsync(
            OrderId orderId,
            CancellationToken cancellationToken)
        {
            var order = await GetRequiredOrderAsync(
                orderId,
                cancellationToken);

            order.Cancel(_clock.UtcNow);

            await _repository.SaveChangesAsync(
                cancellationToken);

            return Map(order);
        }

        private async Task<Order> GetRequiredOrderAsync(
            OrderId orderId,
            CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(
                orderId,
                cancellationToken);

            return order
                ?? throw new KeyNotFoundException(
                    $"Order '{orderId}' was not found.");
        }

        private static OrderResponse Map(Order order)
            => new(
                order.Id,
                order.CustomerId,
                order.Total.Amount,
                order.Status,
                order.CreatedAt);
    }
}
