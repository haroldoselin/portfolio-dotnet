using AsyncAwait.Showcase.Application.Contracts;

namespace AsyncAwait.Showcase.Infrastructure;

public sealed class InMemoryOrderDataSource(TimeSpan latency) : IOrderDataSource
{
    public Task<decimal> GetTotalAmountAsync(int customerId, CancellationToken cancellationToken) =>
        ExecuteAsync(1250m + customerId, cancellationToken);

    public Task<int> GetOrderCountAsync(int customerId, CancellationToken cancellationToken) =>
        ExecuteAsync(3 + customerId % 4, cancellationToken);

    public ValueTask<bool> IsCustomerActiveAsync(int customerId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return ValueTask.FromResult(customerId != 404);
    }

    private async Task<T> ExecuteAsync<T>(T result, CancellationToken cancellationToken)
    {
        await Task.Delay(latency, cancellationToken).ConfigureAwait(false);
        return result;
    }
}