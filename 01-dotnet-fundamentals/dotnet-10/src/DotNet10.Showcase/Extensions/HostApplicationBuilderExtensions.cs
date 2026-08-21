using Microsoft.Extensions.Hosting;

namespace DotNet10.Showcase.Extensions
{
    public static class HostApplicationBuilderExtensions
    {
        public static HostApplicationBuilder AddShowcaseServices(
        this HostApplicationBuilder builder)
        {
            builder.Services
                .AddApplication()
                .AddInfrastructure(builder.Configuration)
                .AddHosting();

            return builder;
        }
    }
}
