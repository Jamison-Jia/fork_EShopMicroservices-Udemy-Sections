using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrderHandler(IApplicationDbContext dbContext) : 
    IQueryHandler<GetOrdersQuery, GetOrderResult>
{
    public async Task<GetOrderResult> Handle(GetOrdersQuery request, 
        CancellationToken cancellationToken)
    {
        // get order-entities from db
        // map order-entities to DTOs
        // return result

        var orders = await dbContext.Orders.ToListAsync(cancellationToken);

        return new GetOrderResult(orders.ToOrderDtoList());
    }
}