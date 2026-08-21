using AsyncAwait.Showcase.Application.Contracts;
using AsyncAwait.Showcase.Application.Services;
using Xunit;

namespace AsyncAwait.Showcase.Tests;

public sealed class OrderSummaryServiceTests
{
    [Fact]
    public async Task CreateAsync_ReturnsCombinedSummary()
    {
        var service = CreateService(new TestDataSource());

        var result = await service.CreateAsync(42);

        Assert.Equal(42, result.CustomerId);
        Assert.Equal(1250m, result.TotalAmount);
        Assert.Equal(3, result.OrderCount);
    }

    [Fact]
    public async Task CreateAsync_StartsIndependentOperationsConcurrently()
    {
        var source = new CoordinatedDataSource();
        var service = CreateService(source);
        var operation = service.CreateAsync(42);

        await source.BothOperationsStarted.Task;
        source.Release();

        var result = await operation;

        Assert.Equal(1250m, result.TotalAmount);
        Assert.Equal(3, result.OrderCount);
    }

    [Fact]
    public async Task CreateAsync_PropagatesCancellation()
    {
        var service = CreateService(new TestDataSource(TimeSpan.FromSeconds(5)));
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            service.CreateAsync(42, cancellation.Token));
    }

    [Fact]
    public async Task CreateAsync_RejectsInactiveCustomer()
    {
        var service = CreateService(new TestDataSource(isActive: false));

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.CreateAsync(42));

        Assert.Contains("inactive", exception.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task CreateAsync_PropagatesDataSourceFailure()
    {
        var service = CreateService(new TestDataSource(exception: new InvalidOperationException("Data source unavailable")));

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.CreateAsync(42));

        Assert.Equal("Data source unavailable", exception.Message);
    }

    private static OrderSummaryService CreateService(IOrderDataSource source) =>
        new(source, TimeProvider.System);

    private sealed class TestDataSource(
        TimeSpan? delay = null,
        bool isActive = true,
        Exception? exception = null) : IOrderDataSource
    {
        public Task<decimal> GetTotalAmountAsync(int customerId, CancellationToken cancellationToken) =>
            ExecuteAsync(1250m, cancellationToken);

        public Task<int> GetOrderCountAsync(int customerId, CancellationToken cancellationToken) =>
            ExecuteAsync(3, cancellationToken);

        public ValueTask<bool> IsCustomerActiveAsync(int customerId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return ValueTask.FromResult(isActive);
        }

        private async Task<T> ExecuteAsync<T>(T result, CancellationToken cancellationToken)
        {
            if (exception is not null)
            {
                throw exception;
            }

            if (delay is not null)
            {
                await Task.Delay(delay.Value, cancellationToken);
            }

            return result;
        }
    }

    private sealed class CoordinatedDataSource : IOrderDataSource
    {
        private readonly TaskCompletionSource<bool> totalStarted =
            new(TaskCreationOptions.RunContinuationsAsynchronously);
        private readonly TaskCompletionSource<bool> countStarted =
            new(TaskCreationOptions.RunContinuationsAsynchronously);
        private readonly TaskCompletionSource<bool> release =
            new(TaskCreationOptions.RunContinuationsAsynchronously);

        public TaskCompletionSource<bool> BothOperationsStarted { get; } =
            new(TaskCreationOptions.RunContinuationsAsynchronously);

        public async Task<decimal> GetTotalAmountAsync(int customerId, CancellationToken cancellationToken)
        {
            totalStarted.SetResult(true);
            await WaitForBothOperationsAsync(cancellationToken);
            return 1250m;
        }

        public async Task<int> GetOrderCountAsync(int customerId, CancellationToken cancellationToken)
        {
            countStarted.SetResult(true);
            await WaitForBothOperationsAsync(cancellationToken);
            return 3;
        }

        public ValueTask<bool> IsCustomerActiveAsync(int customerId, CancellationToken cancellationToken) =>
            ValueTask.FromResult(true);

        public void Release() => release.SetResult(true);

        private async Task WaitForBothOperationsAsync(CancellationToken cancellationToken)
        {
            await Task.WhenAll(totalStarted.Task, countStarted.Task).WaitAsync(cancellationToken);
            BothOperationsStarted.TrySetResult(true);
            await release.Task.WaitAsync(cancellationToken);
        }
    }
}