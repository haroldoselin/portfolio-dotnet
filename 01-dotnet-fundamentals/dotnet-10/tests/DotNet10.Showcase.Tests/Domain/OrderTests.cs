using System;
using System.Linq;
using DotNet10.Showcase.Domain.Entities;
using DotNet10.Showcase.Domain.Enuns;
using DotNet10.Showcase.Domain.Events;
using DotNet10.Showcase.Domain.ValueObjects;
using DotNet10.Showcase.Domain.Exceptions;
using Xunit;

namespace DotNet10.Showcase.Tests.Domain
{
    public sealed class OrderTests
    {
        [Fact]
        public void ShouldCreateOrder()
        {
            // Arrange
            var customerId = "customer-1";
            var total = new Money(100m);
            var createdAt = DateTimeOffset.UtcNow;

            // Act
            var order = Order.Create(customerId, total, createdAt);

            // Assert
            Assert.False(order.Id.IsEmpty);
            Assert.Equal(customerId, order.CustomerId);
            Assert.Equal(total.Amount, order.Total.Amount);
            Assert.Equal(OrderStatus.Pending, order.Status);
            Assert.Single(order.DomainEvents);
            Assert.IsType<OrderCreatedDomainEvent>(order.DomainEvents.Single());
        }

        [Fact]
        public void ShouldConfirmOrder_AddsConfirmedEvent()
        {
            // Arrange
            var order = Order.Create("c1", new Money(50m), DateTimeOffset.UtcNow);
            var confirmedAt = DateTimeOffset.UtcNow;

            // Act
            order.Confirm(confirmedAt);

            // Assert
            Assert.Equal(OrderStatus.Confirmed, order.Status);
            Assert.Equal(confirmedAt, order.ConfirmedAt);
            Assert.Contains(order.DomainEvents, e => e is OrderConfirmedDomainEvent);
        }

        [Fact]
        public void ShouldStartProcessing_ThenComplete()
        {
            // Arrange
            var now = DateTimeOffset.UtcNow;
            var order = Order.Create("c1", new Money(75m), now);
            order.Confirm(now);

            // Act
            order.StartProcessing(now);
            order.Complete(now);

            // Assert
            Assert.Equal(OrderStatus.Completed, order.Status);
            Assert.Equal(now, order.ProcessingStartedAt);
            Assert.Equal(now, order.CompletedAt);
        }

        [Fact]
        public void CancelAfterCompleted_ShouldThrowInvalidOrderStateException()
        {
            // Arrange
            var now = DateTimeOffset.UtcNow;
            var order = Order.Create("c1", new Money(20m), now);
            order.Confirm(now);
            order.StartProcessing(now);
            order.Complete(now);

            // Act & Assert
            _ = Assert.Throws<InvalidOrderStateException>(() => order.Cancel(now));
        }
    }
}
