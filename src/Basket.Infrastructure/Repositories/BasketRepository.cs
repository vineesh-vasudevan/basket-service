
using Basket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Basket.Domain.Entities;
using Basket.Domain.Repositories;
using CSharpFunctionalExtensions;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository(ApplicationDbContext dbContext) : IBasketRepository
    {
        public async Task AddAsync(Domain.Entities.Basket basket, CancellationToken cancellationToken)
        {
            await dbContext.Basket.AddAsync(basket, cancellationToken);
        }

        public async Task<Maybe<Domain.Entities.Basket>> GetBasket(Guid id, CancellationToken cancellationToken)
        {
            var basket = await dbContext.Basket
                .Include(b => b.BasketItems)
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted, cancellationToken);

            return Maybe.From(basket);
        }

        public void Update(Domain.Entities.Basket basket)
        {
            dbContext.Basket.Update(basket);
        }

        public async Task AddBasketItemAsync(BasketItem basketItem, CancellationToken cancellationToken)
        {
            await dbContext.BasketItem.AddAsync(basketItem, cancellationToken);
        }

        public void UpdateBasketItem(BasketItem basketItem)
        {
            dbContext.BasketItem.Update(basketItem);
        }

        public Task CreateBasketItem(Domain.Entities.Basket basket, BasketItem basketItem, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}