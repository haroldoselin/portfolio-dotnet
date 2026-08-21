using DotNet10.Showcase.Application.Models;
using DotNet10.Showcase.Domain.ValueObjects;

namespace DotNet10.Showcase.Application.Contracts
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateAsync(
            CreateOrderRequest request,
            CancellationToken cancellationToken);

        Task<OrderResponse> ConfirmAsync(
            OrderId orderId,
            CancellationToken cancellationToken);

        Task<ProcessingResult> ProcessAsync(
            OrderId orderId,
            CancellationToken cancellationToken);

        Task<OrderResponse> CompleteAsync(
            OrderId orderId,
            CancellationToken cancellationToken);

        Task<OrderResponse> CancelAsync(
            OrderId orderId,
            CancellationToken cancellationToken);
    }
}
