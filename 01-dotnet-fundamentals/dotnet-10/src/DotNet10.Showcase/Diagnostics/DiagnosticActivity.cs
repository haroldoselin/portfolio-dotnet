using System.Diagnostics;

namespace DotNet10.Showcase.Diagnostics
{
    public static class DiagnosticActivity
    {
        private static readonly ActivitySource Source =
            new(
                DiagnosticConstants.ActivitySourceName,
                DiagnosticConstants.ActivitySourceVersion);

        public static Activity? StartOrderProcessing(
            string orderId)
        {
            var activity = Source.StartActivity(
                "order.process",
                ActivityKind.Internal);

            activity?.SetTag(
                "order.id",
                orderId);

            return activity;
        }
    }
}
