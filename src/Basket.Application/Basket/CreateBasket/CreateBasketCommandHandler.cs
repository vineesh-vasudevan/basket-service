using Basket.Domain.Entities;
using Basket.Domain.Repositories;
using Basket.Shared.CQRS;

namespace Basket.Application.Basket.CreateBasket
{
    public class CreateBasketCommandHandler(IBasketRepository basketRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateBasketCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
        {
            var request = command.CreateBasketRequest;
            var basket = Domain.Entities.Basket.Create(
                Guid.NewGuid(),
                request.UserId,
                request.Currency,
                request.Country,
                request.SessionId
            );

            foreach (var item in request.BasketItems)
            {
                var basketItem = BasketItem.Create(
                    basket.Id,
                    Guid.NewGuid(),
                    item.ProductCode,
                    item.Color,
                    item.Price,
                    item.Quantity
                );

                basket.AddItem(basketItem);
            }

            await basketRepository.AddAsync(basket, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return basket.Id;
        }
    }
}
