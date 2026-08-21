using System;
using System.Collections.Generic;
using System.Text;
using DotNet10.Showcase.Domain.Enuns;
using DotNet10.Showcase.Domain.Events;
using DotNet10.Showcase.Domain.Exceptions;
using DotNet10.Showcase.Domain.ValueObjects;

namespace DotNet10.Showcase.Domain.Entities
{
    public sealed class Order
    {
        private readonly List<object> _domainEvents = [];

        private Order(
            OrderId id,
            string customerId,
            Money total,
            DateTimeOffset createdAt)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentException(
                    "Customer ID is required.",
                    nameof(customerId));
            }

            if (total.Amount <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(total),
                    "Order total must be greater than zero.");
            }

            Id = id;
            CustomerId = customerId;
            Total = total;
            CreatedAt = createdAt;
            Status = OrderStatus.Pending;
        }

        public OrderId Id { get; }

        public string CustomerId { get; }

        public Money Total { get; }

        public OrderStatus Status { get; private set; }

        public DateTimeOffset CreatedAt { get; }

        public DateTimeOffset? ConfirmedAt { get; private set; }

        public DateTimeOffset? ProcessingStartedAt { get; private set; }

        public DateTimeOffset? CompletedAt { get; private set; }

        public DateTimeOffset? CancelledAt { get; private set; }

        public IReadOnlyCollection<object> DomainEvents
            => _domainEvents.AsReadOnly();

        public static Order Create(
            string customerId,
            Money total,
            DateTimeOffset createdAt)
        {
            var order = new Order(
                OrderId.New(),
                customerId,
                total,
                createdAt);

            order.AddDomainEvent(
                new OrderCreatedDomainEvent(
                    order.Id,
                    order.CustomerId,
                    createdAt));

            return order;
        }

        public void Confirm(DateTimeOffset confirmedAt)
        {
            EnsureStatus(OrderStatus.Pending, nameof(Confirm));

            Status = OrderStatus.Confirmed;
            ConfirmedAt = confirmedAt;

            AddDomainEvent(
                new OrderConfirmedDomainEvent(
                    Id,
                    confirmedAt));
        }

        public void StartProcessing(DateTimeOffset startedAt)
        {
            EnsureStatus(OrderStatus.Confirmed, nameof(StartProcessing));

            Status = OrderStatus.Processing;
            ProcessingStartedAt = startedAt;
        }

        public void Complete(DateTimeOffset completedAt)
        {
            EnsureStatus(OrderStatus.Processing, nameof(Complete));

            Status = OrderStatus.Completed;
            CompletedAt = completedAt;
        }

        public void Cancel(DateTimeOffset cancelledAt)
        {
            if (Status is OrderStatus.Completed or OrderStatus.Cancelled)
            {
                throw new InvalidOrderStateException(
                    nameof(Cancel),
                    Status.ToString());
            }

            Status = OrderStatus.Cancelled;
            CancelledAt = cancelledAt;
        }

        public void ClearDomainEvents()
            => _domainEvents.Clear();

        private void EnsureStatus(
            OrderStatus expectedStatus,
            string operation)
        {
            if (Status != expectedStatus)
            {
                throw new InvalidOrderStateException(
                    operation,
                    Status.ToString());
            }
        }

        private void AddDomainEvent(object domainEvent)
            => _domainEvents.Add(domainEvent);
    }
}
