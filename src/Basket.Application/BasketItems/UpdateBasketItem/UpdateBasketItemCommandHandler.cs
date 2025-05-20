namespace Basket.Application.BasketItems.UpdateBasketItem
{
    public class UpdateBasketItemCommandHandler
        (IBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper mapper)
            : ICommandHandler<UpdateBasketItemCommand, BasketDto>
    {
        public async Task<BasketDto> Handle(UpdateBasketItemCommand command, CancellationToken cancellationToken)
        {
            var maybeBasket = await basketRepository.GetBasket(command.BasketId, cancellationToken);
            if (maybeBasket.HasNoValue)
                throw new BasketNotFoundException(command.BasketId);

            var basket = maybeBasket.Value;

            var maybeItem = basket.GetBasketItem(command.ItemId);
            if (maybeItem.HasNoValue)
                throw new BasketItemNotFoundException(command.ItemId);

            var basketItem = maybeItem.Value;

            basket.UpdateItem(basketItem.Id, command.Quantity);

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                basketRepository.Update(basket);
                basketRepository.UpdateBasketItem(basketItem); //TODO to remove
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