namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery : IQuery<GetOrderResult>
{
    
}

public record GetOrderResult(IEnumerable<OrderDto> Orders);