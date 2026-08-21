namespace AsyncAwait.Showcase.Application.Contracts;

public interface IOrderDataSource
{
    Task<decimal> GetTotalAmountAsync(int customerId, CancellationToken cancellationToken);

    Task<int> GetOrderCountAsync(int customerId, CancellationToken cancellationToken);

    ValueTask<bool> IsCustomerActiveAsync(int customerId, CancellationToken cancellationToken);
}