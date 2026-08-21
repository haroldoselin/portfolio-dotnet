using AsyncAwait.Showcase.Application.Contracts;
using AsyncAwait.Showcase.Application.Models;

namespace AsyncAwait.Showcase.Application.Services;

public sealed class OrderSummaryService(IOrderDataSource dataSource, TimeProvider timeProvider)
{
    public async Task<OrderSummary> CreateAsync(
        int customerId,
        CancellationToken cancellationToken = default)
    {
        if (customerId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(customerId));
        }

        var active = await dataSource
            .IsCustomerActiveAsync(customerId, cancellationToken)
            .ConfigureAwait(false);

        if (!active)
        {
            throw new InvalidOperationException($"Customer '{customerId}' is inactive.");
        }

        var totalTask = dataSource.GetTotalAmountAsync(customerId, cancellationToken);
        var countTask = dataSource.GetOrderCountAsync(customerId, cancellationToken);

        await Task.WhenAll(totalTask, countTask).ConfigureAwait(false);

        return new OrderSummary(
            customerId,
            await totalTask.ConfigureAwait(false),
            await countTask.ConfigureAwait(false),
            timeProvider.GetUtcNow());
    }
}