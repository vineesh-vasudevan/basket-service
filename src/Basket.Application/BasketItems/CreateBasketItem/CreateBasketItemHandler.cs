using Basket.Domain.Entities;
using Basket.Domain.Enum;
using Basket.Domain.Exceptions;
using Basket.Domain.Repositories;
using Basket.Shared.CQRS;

namespace Basket.Application.BasketItems.CreateBasketItem
{
    public class CreateBasketItemHandler
        (IBasketRepository basketRepository, IUnitOfWork unitOfWork)
            : ICommandHandler<CreateBasketItemCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBasketItemCommand command, CancellationToken cancellationToken)
        {
            var maybeBasket = await basketRepository.GetBasket(command.BasketId, cancellationToken);

            if (maybeBasket.HasNoValue)
            {
                throw new BasketNotFoundException(command.BasketId);
            }

            var basket = maybeBasket.Value;
            await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var basketItem = BasketItem.Create(
                    basket.Id,
                    Guid.NewGuid(),
                    command.Request.ProductCode,
                    command.Request.Color,
                    command.Request.Price,
                    command.Request.Quantity,
                    BasketItemStatus.FromName(command.Request.Status.ToString())
                );

                basketRepository.Update(basket);
                await basketRepository.AddBasketItemAsync(basketItem, cancellationToken);

                await unitOfWork.SaveChangesAsync(cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);
                return basketItem.Id;
            }
            catch
            {
                await unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}