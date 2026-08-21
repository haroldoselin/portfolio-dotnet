using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using DotNet10.Showcase.Hosting.HostedServices;
using DotNet10.Showcase.Infrastructure.Persistence;
using DotNet10.Showcase.Infrastructure.Configuration;
using DotNet10.Showcase.Application.Models;
using DotNet10.Showcase.Domain.ValueObjects;
using DotNet10.Showcase.Domain.Entities;
using DotNet10.Showcase.Application.Contracts;
using Xunit;

namespace DotNet10.Showcase.Tests.Hosting
{
    public sealed class OrderProcessingWorkerTests
    {
        private sealed class FakeOrderService : IOrderService
        {
            public int Calls { get; private set; }

            public Task<OrderResponse> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task<OrderResponse> ConfirmAsync(OrderId orderId, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task<ProcessingResult> ProcessAsync(OrderId orderId, CancellationToken cancellationToken)
            {
                Calls++;
                return Task.FromResult(new ProcessingResult(orderId, true, "ok"));
            }
            public Task<OrderResponse> CompleteAsync(OrderId orderId, CancellationToken cancellationToken) => throw new NotImplementedException();
            public Task<OrderResponse> CancelAsync(OrderId orderId, CancellationToken cancellationToken) => throw new NotImplementedException();
        }

        [Fact]
        public async Task ProcessBatch_Calls_OrderService_For_Pending_Orders()
        {
            // Arrange
            var repo = new InMemoryOrderRepository();
            var now = DateTimeOffset.UtcNow;
            var order = Order.Create("c1", new Money(30m), now);
            order.Confirm(now);
            await repo.AddAsync(order, CancellationToken.None);

            var options = Options.Create(new OrderProcessingOptions { BatchSize = 10, PollingIntervalSeconds = 1 });
            var fake = new FakeOrderService();
            var logger = NullLogger<OrderProcessingWorker>.Instance;

            var worker = new OrderProcessingWorker(repo, fake, options, logger);

            // Act - call private ProcessBatchAsync via reflection
            var method = typeof(OrderProcessingWorker).GetMethod("ProcessBatchAsync", BindingFlags.NonPublic | BindingFlags.Instance)!;
            await (Task)method.Invoke(worker, new object[] { CancellationToken.None })!;

            // Assert
            Assert.Equal(1, fake.Calls);
        }

        [Fact]
        public async Task ProcessBatch_Respects_Cancellation()
        {
            var repo = new InMemoryOrderRepository();
            var now = DateTimeOffset.UtcNow;
            var order = Order.Create("c1", new Money(30m), now);
            order.Confirm(now);
            await repo.AddAsync(order, CancellationToken.None);

            var options = Options.Create(new OrderProcessingOptions { BatchSize = 10, PollingIntervalSeconds = 1 });
            var fake = new FakeOrderService();
            var logger = NullLogger<OrderProcessingWorker>.Instance;

            var worker = new OrderProcessingWorker(repo, fake, options, logger);

            var cts = new CancellationTokenSource();
            cts.Cancel();

            var method = typeof(OrderProcessingWorker).GetMethod("ProcessBatchAsync", BindingFlags.NonPublic | BindingFlags.Instance)!;

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => (Task)method.Invoke(worker, new object[] { cts.Token })!);
        }
    }
}
