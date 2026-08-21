using AsyncAwait.Showcase.Application.Services;
using AsyncAwait.Showcase.Infrastructure;

var service = new OrderSummaryService(
    new InMemoryOrderDataSource(TimeSpan.FromMilliseconds(100)),
    TimeProvider.System);

Console.WriteLine("======================================");
Console.WriteLine(" ASYNC/AWAIT - .NET 10 SENIOR SHOWCASE");
Console.WriteLine("======================================");

var summary = await service.CreateAsync(42);
Console.WriteLine($"Cliente: {summary.CustomerId}");
Console.WriteLine($"Pedidos: {summary.OrderCount}");
Console.WriteLine($"Total: {summary.TotalAmount:C}");

using var cancellation = new CancellationTokenSource(TimeSpan.FromMilliseconds(10));

try
{
    await service.CreateAsync(42, cancellation.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operação cancelada de forma cooperativa.");
}