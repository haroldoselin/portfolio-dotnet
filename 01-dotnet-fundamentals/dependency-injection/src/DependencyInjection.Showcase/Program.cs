using DependencyInjection.Showcase.Application.Contracts;
using DependencyInjection.Showcase.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();

services.AddLogging(builder => builder.AddSimpleConsole(options =>
{
    options.SingleLine = true;
}));
services.AddShowcaseServices();

using var provider = services.BuildServiceProvider(new ServiceProviderOptions
{
    ValidateScopes = true,
    ValidateOnBuild = true
});

Console.WriteLine("============================================");
Console.WriteLine(" DEPENDENCY INJECTION - .NET 10 SHOWCASE");
Console.WriteLine("============================================");

using var scope = provider.CreateScope();
var processor = scope.ServiceProvider.GetRequiredService<IOrderProcessor>();
var processedOrder = processor.Process(1001);

Console.WriteLine($"Pedido: {processedOrder.Id}");
Console.WriteLine($"Valor: {processedOrder.Amount:C}");
Console.WriteLine($"Status: {processedOrder.Status}");