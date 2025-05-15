using Basket.Domain.Entities;
using Basket.Domain.Enum;
using Basket.Domain.Repositories;
using Basket.Infrastructure.Data;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository(ApplicationDbContext dbContext) : IBasketRepository
    {
        public async Task AddAsync(Domain.Entities.Basket basket, CancellationToken cancellationToken)
        {
            await dbContext.Baskets.AddAsync(basket, cancellationToken);
        }

        public async Task<Maybe<Domain.Entities.Basket>> GetBasket(Guid id, CancellationToken cancellationToken)
        {
            var basket = await dbContext.Baskets
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.Id == id && b.Status != BasketStatus.Cancelled, cancellationToken);

            return Maybe.From(basket);
        }

        public void Update(Domain.Entities.Basket basket)
        {
            dbContext.Baskets.Update(basket);
        }

        public async Task AddBasketItemAsync(BasketItem basketItem, CancellationToken cancellationToken)
        {
            await dbContext.BasketItems.AddAsync(basketItem, cancellationToken);
        }

        public void UpdateBasketItem(BasketItem basketItem)
        {
            dbContext.BasketItems.Update(basketItem);
        }

        public Task CreateBasketItem(Domain.Entities.Basket basket, BasketItem basketItem, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}