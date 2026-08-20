using CSharp14.Showcase.Domain.ValueObjects;
using Xunit;

namespace CSharp14.Showcase.Tests.Domain
{
    public sealed class MoneyTests
    {
        [Fact]
        public void ShouldIdentifyPositiveValue()
        {
            var money = new Money(100m);

            Assert.True(money.IsPositive);
            Assert.False(money.IsNegative);
        }

        [Fact]
        public void ShouldIdentifyNegativeValue()
        {
            var money = new Money(-100m);

            Assert.False(money.IsPositive);
            Assert.True(money.IsNegative);
        }

        [Fact]
        public void ShouldAddTwoMoneyValues()
        {
            var first = new Money(100m);
            var second = new Money(50m);

            var result = first + second;

            Assert.Equal(150m, result.Amount);
        }

        [Fact]
        public void ShouldSubtractTwoMoneyValues()
        {
            var first = new Money(100m);
            var second = new Money(40m);

            var result = first - second;

            Assert.Equal(60m, result.Amount);
        }

        [Fact]
        public void ZeroShouldReturnZeroValue()
        {
            var result = Money.Zero;

            Assert.Equal(0m, result.Amount);
            Assert.True(result.IsZero);
        }
    }
}
