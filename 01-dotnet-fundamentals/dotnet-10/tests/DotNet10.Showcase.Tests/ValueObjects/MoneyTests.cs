using DotNet10.Showcase.Domain.ValueObjects;
using Xunit;

namespace DotNet10.Showcase.Tests.ValueObjects
{
    public sealed class MoneyTests
    {
        [Fact]
        public void Operators_And_Properties_Work_As_Expected()
        {
            // Arrange
            var a = new Money(10m);
            var b = new Money(5.5m);

            // Act
            var sum = a + b;
            var diff = a - b;
            var mult = b * 2m;

            // Assert
            Assert.Equal(15.5m, sum.Amount);
            Assert.Equal(4.5m, diff.Amount);
            Assert.Equal(11.0m, mult.Amount);
            Assert.True(a.IsPositive);
            Assert.False(a.IsNegative);
            Assert.False(Money.Zero.IsPositive);
            Assert.True(Money.Zero.IsZero);
            // ToString uses current culture formatting; parse back using current culture to verify numeric value
            Assert.Equal(b.Amount, decimal.Parse(b.ToString(), System.Globalization.CultureInfo.CurrentCulture));
        }
    }
}
