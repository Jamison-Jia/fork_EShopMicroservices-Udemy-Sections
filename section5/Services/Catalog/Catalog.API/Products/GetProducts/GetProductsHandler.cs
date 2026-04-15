using Marten.Linq;

namespace Catalog.API.Products.GetProducts;

public record GetProductQuery() : IQuery<GetProductResult>;
public record GetProductResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query,
                                         CancellationToken cancellationToken)
    {
        var products =
            await ((IMartenQueryable)session.Query<Product>())
                                            .ToListAsync<Product>(cancellationToken);

        return new GetProductResult(products);
    }
}