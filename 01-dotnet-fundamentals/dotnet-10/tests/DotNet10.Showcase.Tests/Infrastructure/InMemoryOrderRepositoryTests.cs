using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DotNet10.Showcase.Domain.Entities;
using DotNet10.Showcase.Domain.ValueObjects;
using DotNet10.Showcase.Infrastructure.Persistence;
using DotNet10.Showcase.Domain.Enuns;
using Xunit;

namespace DotNet10.Showcase.Tests.Infrastructure
{
    public sealed class InMemoryOrderRepositoryTests
    {
        [Fact]
        public async Task AddAsync_Then_GetByIdAsync_Returns_Order()
        {
            // Arrange
            var repo = new InMemoryOrderRepository();
            var order = Order.Create("c1", new Money(10m), DateTimeOffset.UtcNow);

            // Act
            await repo.AddAsync(order, CancellationToken.None);
            var fetched = await repo.GetByIdAsync(order.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(fetched);
            Assert.Equal(order.Id, fetched!.Id);
        }

        [Fact]
        public async Task AddAsync_Duplicate_Throws()
        {
            var repo = new InMemoryOrderRepository();
            var order = Order.Create("c1", new Money(10m), DateTimeOffset.UtcNow);

            await repo.AddAsync(order, CancellationToken.None);

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => repo.AddAsync(order, CancellationToken.None));
        }

        [Fact]
        public async Task GetPendingAsync_Returns_Confirmed_Orders()
        {
            // Arrange
            var repo = new InMemoryOrderRepository();
            var now = DateTimeOffset.UtcNow;
            var order = Order.Create("c1", new Money(30m), now);
            order.Confirm(now);

            await repo.AddAsync(order, CancellationToken.None);

            // Act
            var pending = await repo.GetPendingAsync(10, CancellationToken.None);

            // Assert
            Assert.Single(pending);
            Assert.Equal(OrderStatus.Confirmed, pending.First().Status);
        }
    }
}
