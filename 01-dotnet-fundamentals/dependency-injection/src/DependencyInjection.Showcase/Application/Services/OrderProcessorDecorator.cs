using DependencyInjection.Showcase.Application.Contracts;
using DependencyInjection.Showcase.Application.Models;
using DependencyInjection.Showcase.Domain;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Showcase.Application.Services;

public sealed class OrderProcessorDecorator(
    OrderProcessor processor,
    ILogger<OrderProcessorDecorator> logger,
    OperationContext operationContext) : IOrderProcessor
{
    public Order Process(int orderId)
    {
        logger.LogInformation(
            "Processsando {OrderId} com correlation {CorrelationId}",
            orderId,
            operationContext.CorrelationId);

        var result = processor.Process(orderId);

        logger.LogInformation("Pedido {OrderId} processado com status {Status}", orderId, result.Status);
        return result;
    }
}