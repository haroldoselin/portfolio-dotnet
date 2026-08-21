using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNet10.Showcase.Application.Services;
using DotNet10.Showcase.Infrastructure.Persistence;
using DotNet10.Showcase.Infrastructure.Time;
using DotNet10.Showcase.Application.Models;
using DotNet10.Showcase.Domain.ValueObjects;
using DotNet10.Showcase.Domain.Entities;
using DotNet10.Showcase.Domain.Exceptions;
using Xunit;

namespace DotNet10.Showcase.Tests.Application
{
    public sealed class OrderServiceTests
    {
        private sealed class TestClock : ISystemClock
        {
            public DateTimeOffset UtcNow { get; set; }
        }

        [Fact]
        public async Task CreateAsync_Adds_Order_And_Returns_Response()
        {
            // Arrange
            var repo = new InMemoryOrderRepository();
            var clock = new TestClock { UtcNow = DateTimeOffset.UtcNow };
            var service = new OrderService(repo, clock);
            var request = new CreateOrderRequest("c1", 45.5m);

            // Act
            var response = await service.CreateAsync(request, CancellationToken.None);
            var stored = await repo.GetByIdAsync(response.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(stored);
            Assert.Equal(response.Id, stored!.Id);
            Assert.Equal(request.CustomerId, response.CustomerId);
            Assert.Equal(request.Total, response.Total);
        }

        [Fact]
        public async Task ConfirmAsync_When_NotFound_Throws()
        {
            var repo = new InMemoryOrderRepository();
            var clock = new TestClock { UtcNow = DateTimeOffset.UtcNow };
            var service = new OrderService(repo, clock);

            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => service.ConfirmAsync(OrderId.New(), CancellationToken.None));
        }

        [Fact]
        public async Task ProcessAsync_StartsProcessing_Returns_Result()
        {
            var repo = new InMemoryOrderRepository();
            var clock = new TestClock { UtcNow = DateTimeOffset.UtcNow };
            var service = new OrderService(repo, clock);

            var order = Order.Create("c1", new Money(10m), clock.UtcNow);
            order.Confirm(clock.UtcNow);
            await repo.AddAsync(order, CancellationToken.None);

            var result = await service.ProcessAsync(order.Id, CancellationToken.None);

            Assert.True(result.Success);
            var stored = await repo.GetByIdAsync(order.Id, CancellationToken.None);
            Assert.Equal(DotNet10.Showcase.Domain.Enuns.OrderStatus.Processing, stored!.Status);
        }

        [Fact]
        public async Task CancelAsync_After_Complete_Throws_InvalidOrderStateException()
        {
            var repo = new InMemoryOrderRepository();
            var clock = new TestClock { UtcNow = DateTimeOffset.UtcNow };
            var service = new OrderService(repo, clock);

            var order = Order.Create("c1", new Money(20m), clock.UtcNow);
            order.Confirm(clock.UtcNow);
            order.StartProcessing(clock.UtcNow);
            order.Complete(clock.UtcNow);
            await repo.AddAsync(order, CancellationToken.None);

            await Assert.ThrowsAsync<InvalidOrderStateException>(
                () => service.CancelAsync(order.Id, CancellationToken.None));
        }
    }
}
