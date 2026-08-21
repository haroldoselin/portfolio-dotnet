using CSharp14.Showcase.Domain.Entities;
using CSharp14.Showcase.Domain.Enums;
using CSharp14.Showcase.Domain.ValueObjects;
using Xunit;

namespace CSharp14.Showcase.Tests.Domain
{
    public sealed class TransactionTests
    {
        [Fact]
        public void ShouldCreateTransaction()
        {
            var transaction = CreateTransaction();

            Assert.NotEqual(Guid.Empty, transaction.Id);
            Assert.Equal("Supermercado", transaction.Description);
            Assert.Equal(-150.75m, transaction.Amount.Amount);
            Assert.Equal(
                TransactionType.Gasto,
                transaction.Type);
        }

        [Fact]
        public void ShouldCategorizeTransaction()
        {
            var transaction = CreateTransaction();

            transaction.Categorize(" Alimentação ");

            Assert.Equal("Alimentação", transaction.Category);
            Assert.True(transaction.IsCategorized);
        }

        [Fact]
        public void ShouldRemoveCategory()
        {
            var transaction = CreateTransaction();

            transaction.Categorize("Alimentação");
            transaction.RemoveCategory();

            Assert.Null(transaction.Category);
            Assert.False(transaction.IsCategorized);
        }

        [Fact]
        public void ShouldRejectEmptyCategory()
        {
            var transaction = CreateTransaction();

            _ = Assert.Throws<ArgumentException>(
                () => transaction.Categorize(" "));
        }

        private static Transaction CreateTransaction()
        {
            return new Transaction
            {
                Id = Guid.NewGuid(),
                Description = "Supermercado",
                Amount = new Money(-150.75m),
                Type = TransactionType.Gasto,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
