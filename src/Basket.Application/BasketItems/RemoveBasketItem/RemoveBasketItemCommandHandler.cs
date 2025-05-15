using AutoMapper;
using Basket.Contracts.Models.Basket.Output;
using Basket.Domain.Exceptions;
using Basket.Domain.Repositories;
using Basket.Shared.CQRS;

namespace Basket.Application.BasketItems.RemoveBasketItem
{
    public class RemoveBasketItemCommandHandler
        (IBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper mapper)
        : ICommandHandler<RemoveBasketItemCommand, BasketDto>
    {
        public async Task<BasketDto> Handle(RemoveBasketItemCommand command, CancellationToken cancellationToken)
        {
            var maybeBasket = await basketRepository.GetBasket(command.BasketId, cancellationToken);

            if (maybeBasket.HasNoValue)
            {
                throw new BasketNotFoundException(command.BasketId);
            }

            var basket = maybeBasket.Value;
            var basketItem = basket.GetBasketItem(command.ItemId);

            if (basketItem.HasNoValue)
            {
                throw new BasketItemNotFoundException(command.ItemId);
            }

            basket.DeleteBasketItem(command.ItemId);

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                basketRepository.Update(basket);
                basketRepository.UpdateBasketItem(basketItem.Value); //TODO to remove
                await unitOfWork.SaveChangesAsync(cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);

                return mapper.Map<BasketDto>(basket);
            }
            catch
            {
                await unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}