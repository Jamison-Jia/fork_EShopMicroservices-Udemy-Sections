namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint,
                                      IFeatureManager featureManager,
                                      ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent,
                             CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            // 继续向下游发送消息，如果下游有订阅的话，自动会继续消费、处理这个消息
            OrderDto orderDto = domainEvent.order.ToOrderDto();
            await publishEndpoint.Publish(orderDto, cancellationToken);
        }
    }
}