using DotNet10.Showcase.Domain.ValueObjects;
using Xunit;

namespace DotNet10.Showcase.Tests.ValueObjects
{
    public sealed class OrderIdTests
    {
        [Fact]
        public void New_Is_Not_Empty_And_ToString_Works()
        {
            // Arrange & Act
            var id = OrderId.New();

            // Assert
            Assert.False(id.IsEmpty);
            Assert.False(string.IsNullOrWhiteSpace(id.ToString()));
        }

        [Fact]
        public void Empty_Is_Empty()
        {
            var id = OrderId.Empty;

            Assert.True(id.IsEmpty);
            Assert.Equal(Guid.Empty.ToString(), id.ToString());
        }
    }
}
