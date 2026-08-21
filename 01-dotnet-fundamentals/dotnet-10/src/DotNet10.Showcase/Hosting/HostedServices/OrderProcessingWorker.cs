using DotNet10.Showcase.Application.Contracts;
using DotNet10.Showcase.Infrastructure.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotNet10.Showcase.Hosting.HostedServices
{
    public sealed class OrderProcessingWorker : BackgroundService
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderService _orderService;
        private readonly OrderProcessingOptions _options;
        private readonly ILogger<OrderProcessingWorker> _logger;

        public OrderProcessingWorker(
            IOrderRepository repository,
            IOrderService orderService,
            IOptions<OrderProcessingOptions> options,
            ILogger<OrderProcessingWorker> logger)
        {
            _repository = repository;
            _orderService = orderService;
            _options = options.Value;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Order processing worker started.");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await ProcessBatchAsync(stoppingToken);

                    await Task.Delay(
                        TimeSpan.FromSeconds(
                            _options.PollingIntervalSeconds),
                        stoppingToken);
                }
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(
                    "Order processing worker cancellation requested.");
            }
            finally
            {
                _logger.LogInformation(
                    "Order processing worker stopped.");
            }
        }

        private async Task ProcessBatchAsync(
            CancellationToken cancellationToken)
        {
            var orders = await _repository.GetPendingAsync(
                _options.BatchSize,
                cancellationToken);

            foreach (var order in orders)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    var result = await _orderService.ProcessAsync(
                        order.Id,
                        cancellationToken);

                    _logger.LogInformation(
                        "Order {OrderId} processing result: {Message}",
                        result.OrderId,
                        result.Message);
                }
                catch (Exception exception)
                {
                    _logger.LogError(
                        exception,
                        "Error processing order {OrderId}.",
                        order.Id);
                }
            }
        }
    }
}
