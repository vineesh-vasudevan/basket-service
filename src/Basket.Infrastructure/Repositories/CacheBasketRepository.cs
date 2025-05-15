using Basket.Domain.Entities;
using Basket.Domain.Repositories;
using Basket.Infrastructure.Dtos;
using Basket.Infrastructure.MappingProfile;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Infrastructure.Repositories
{
    public class CacheBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        public async Task AddAsync(Domain.Entities.Basket basket, CancellationToken cancellationToken)
        {
            await basketRepository.AddAsync(basket, cancellationToken);
        }

        public async Task AddBasketItemAsync(BasketItem basketItem, CancellationToken cancellationToken)
        {
            RemoveBasketFromCache(basketItem.BasketId);
            await basketRepository.AddBasketItemAsync(basketItem, cancellationToken);
        }

        public Task CreateBasketItem(Domain.Entities.Basket basket, BasketItem basketItem, CancellationToken cancellationToken)
        {
            RemoveBasketFromCache(basket.Id);
            return basketRepository.CreateBasketItem(basket, basketItem, cancellationToken);
        }

        public async Task<Maybe<Domain.Entities.Basket>> GetBasket(Guid id, CancellationToken cancellationToken)
        {
            var cacheKey = id.ToString();
            var cached = await cache.GetStringAsync(cacheKey, cancellationToken);
            if (!string.IsNullOrEmpty(cached))
            {
                var cachedBasket = JsonSerializer.Deserialize<BasketCacheDto>(cached);
                var basket = BasketCacheMapper.FromDto(cachedBasket!);
                return Maybe.From(basket);
            }

            var basketFromDb = await basketRepository.GetBasket(id, cancellationToken);
            if (basketFromDb.HasValue)
            {
                var basketToCache = BasketCacheMapper.ToDto(basketFromDb.Value!);
                var serialized = JsonSerializer.Serialize(basketToCache);
                await cache.SetStringAsync(cacheKey, serialized, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                }, cancellationToken);
            }

            return basketFromDb;
        }

        public void Update(Domain.Entities.Basket basket)
        {
            RemoveBasketFromCache(basket.Id);
            basketRepository.Update(basket);
        }

        public void UpdateBasketItem(BasketItem basketItem)
        {
            RemoveBasketFromCache(basketItem.BasketId);
            basketRepository.UpdateBasketItem(basketItem);
        }

        private void RemoveBasketFromCache(Guid id)
        {
            var cacheKey = id.ToString();
            cache.Remove(cacheKey);
        }
    }
}