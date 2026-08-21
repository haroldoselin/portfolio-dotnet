using System.Diagnostics;
using DotNet10.Showcase.Diagnostics;
using Xunit;

namespace DotNet10.Showcase.Tests.Diagnostics
{
    public sealed class DiagnosticsTests
    {
        [Fact]
        public void DiagnosticConstants_Have_Expected_Values()
        {
            Assert.False(string.IsNullOrWhiteSpace(DiagnosticConstants.ActivitySourceName));
            Assert.False(string.IsNullOrWhiteSpace(DiagnosticConstants.ActivitySourceVersion));
        }

        [Fact]
        public void StartOrderProcessing_Returns_Activity_Or_Null_But_Not_Throws()
        {
            var activity = DiagnosticActivity.StartOrderProcessing("order-1");

            if (activity is not null)
            {
                Assert.Equal("order-1", activity.Tags.FirstOrDefault(t => t.Key == "order.id").Value);
                activity.Dispose();
            }
        }
    }
}
