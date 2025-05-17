using Basket.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Basket.Domain.Repositories
{
    public interface IBasketRepository
    {
        Task<Maybe<Domain.Entities.Basket>> GetBasket(Guid id, CancellationToken cancellationToken);

        Task AddAsync(Entities.Basket basket, CancellationToken cancellationToken);

        Task CreateBasketItem(Entities.Basket basket, BasketItem basketItem, CancellationToken cancellationToken);

        void UpdateBasketItem(BasketItem basketItem);

        void Update(Domain.Entities.Basket basket);

        Task AddBasketItemAsync(BasketItem basketItem, CancellationToken cancellationToken);
    }
}