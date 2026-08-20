using CSharp14.Showcase.Application.Services;
using Xunit;

namespace CSharp14.Showcase.Tests.Application
{
    public sealed class TransactionAmountParserTests
    {
        private readonly TransactionAmountParser _parser = new();

        [Fact]
        public void ShouldParseValidAmount()
        {
            var result = _parser.TryParse(
                "150.75",
                out var value);

            Assert.True(result);
            Assert.Equal(150.75m, value);
        }

        [Fact]
        public void ShouldRejectInvalidAmount()
        {
            var result = _parser.TryParse(
                "abc",
                out var value);

            Assert.False(result);
            Assert.Equal(0m, value);
        }
    }
}
