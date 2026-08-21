using CSharp14.Showcase.Performance;
using Xunit;

namespace CSharp14.Showcase.Tests.Performance
{
    public sealed class TransactionParserTests
    {
        [Fact]
        public void ShouldParseUsingString()
        {
            var result =
                TransactionParser.ParseWithString("150.75");

            Assert.Equal(150.75m, result);
        }

        [Fact]
        public void ShouldParseUsingReadOnlySpan()
        {
            ReadOnlySpan<char> input = "150.75";

            var result =
                TransactionParser.ParseWithSpan(input);

            Assert.Equal(150.75m, result);
        }
    }
}
