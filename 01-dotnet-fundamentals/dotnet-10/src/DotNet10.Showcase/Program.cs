using DotNet10.Showcase.Extensions;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.AddShowcaseServices();

var host = builder.Build();

await host.RunAsync();