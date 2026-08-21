using CSharp14.Showcase.Application.Services;
using CSharp14.Showcase.Domain.Entities;
using CSharp14.Showcase.Domain.Enums;
using CSharp14.Showcase.Domain.ValueObjects;
using CSharp14.Showcase.Extensions;
using Xunit;

namespace CSharp14.Showcase.Tests.Application
{
    public sealed class TransactionProcessorTests
    {
        private readonly TransactionProcessor _processor = new();

        [Fact]
        public void ShouldReturnUnboundGenericTypeName()
        {
            var result = _processor.GetCollectionTypeName();

            Assert.Equal("List", result);
        }

        [Fact]
        public void ShouldCountTransactions()
        {
            var transactions = CreateTransactions();

            var result = TransactionProcessor.CountTransactions(transactions);

            Assert.Equal(3, result);
        }

        [Fact]
        public void ShouldCalculateTotal()
        {
            var transactions = CreateTransactions();

            var result = TransactionProcessor.CalculateTotal(transactions);

            Assert.Equal(4530m, result);
        }

        [Fact]
        public void ShouldReturnOnlyExpenses()
        {
            var transactions = CreateTransactions();

            var result = _processor.GetExpenses(transactions);

            Assert.Equal(2, result.Count);
            Assert.All(
                result,
                transaction => Assert.True(transaction.IsExpense));
        }

        [Fact]
        public void ShouldProcessTransactions()
        {
            var transactions = CreateTransactions();

            var result = _processor.Process(transactions);

            Assert.Equal("List", result.CollectionType);
            Assert.Equal(3, result.TotalTransactions);
            Assert.Equal(4530m, result.TotalAmount);
            Assert.Equal(2, result.ExpenseCount);
            Assert.Equal(1, result.IncomeCount);
        }

        [Fact]
        public void ShouldRenameCustomerUsingNullConditionalAssignment()
        {
            var customer = new Customer
            {
                Name = "Cliente"
            };

            TransactionProcessor.RenameCustomer(
                customer,
                " Novo Cliente ");

            Assert.Equal("Novo Cliente", customer.Name);
        }

        [Fact]
        public void ShouldAcceptNullCustomer()
        {
            Customer? customer = null;

            var exception = Record.Exception(
                () => TransactionProcessor.RenameCustomer(
                    customer,
                    "Novo Cliente"));

            Assert.Null(exception);
        }

        private static List<Transaction> CreateTransactions()
        {
            return
            [
                new Transaction
            {
                Id = Guid.NewGuid(),
                Description = "Salário",
                Amount = new Money(5000m),
                Type = TransactionType.Renda,
                CreatedAt = DateTime.UtcNow
            },

            new Transaction
            {
                Id = Guid.NewGuid(),
                Description = "Supermercado",
                Amount = new Money(-350m),
                Type = TransactionType.Gasto,
                CreatedAt = DateTime.UtcNow
            },

            new Transaction
            {
                Id = Guid.NewGuid(),
                Description = "Restaurante",
                Amount = new Money(-120m),
                Type = TransactionType.Gasto,
                CreatedAt = DateTime.UtcNow
            }
            ];
        }
    }
}
