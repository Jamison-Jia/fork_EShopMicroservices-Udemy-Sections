using System.Text.Json;
using Basket.Api.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Api.Data;

public class CachedBasketRepository(IDistributedCache cache, IBasketRepository repository) 
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
        if (!string.IsNullOrEmpty(cachedBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        }
        
        var basket = await repository.GetBasket(userName, cancellationToken);
        // add into cache first
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
        
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
    {
        await repository.StoreBasket(basket, cancellationToken);
        
        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
        
        return basket;
    }

    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
    {
        // remove from db
        await repository.DeleteBasket(userName, cancellationToken);
        // remove from cache
        await cache.RemoveAsync(userName, cancellationToken);
        
        return true;
    }
}