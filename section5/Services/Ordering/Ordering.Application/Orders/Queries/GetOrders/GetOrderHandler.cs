using BuildingBlocks.Pagination;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrderHandler(IApplicationDbContext dbContext) : 
    IQueryHandler<GetOrdersQuery, GetOrderResult>
{
    public async Task<GetOrderResult> Handle(GetOrdersQuery query, 
        CancellationToken cancellationToken)
    {
        // get order-entities from db
        // map order-entities to DTOs
        // return result

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        
        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);
        
        var orders = await dbContext.Orders.Include(o => o.OrderItems)
                                                    .OrderBy(o=>o.OrderName.Value)
                                                    .Skip(pageSize * pageIndex)
                                                    .Take(pageSize)
                                                    .ToListAsync(cancellationToken);
        return new GetOrderResult(new PaginatedResult<OrderDto>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));
    }
}