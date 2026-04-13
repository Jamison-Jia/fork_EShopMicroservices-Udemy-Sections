namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResult(Product Product);

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public class GetProductByIdHandler (IDocumentSession session, ILogger<GetProductByIdHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation($"{nameof(GetProductByIdHandler)}.Handle called with {query}");
        
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException();
        }
        
        return new  GetProductByIdResult(product);
    }
}