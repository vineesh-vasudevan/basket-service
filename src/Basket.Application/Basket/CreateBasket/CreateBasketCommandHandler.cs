namespace Basket.Application.Basket.CreateBasket
{
    public class CreateBasketCommandHandler(IBasketRepository basketRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateBasketCommand, Guid>
    {
        public async Task<Guid> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
        {
            var request = command.CreateBasketRequest;
            var basket = Domain.Entities.Basket.Create(
                Guid.NewGuid(),
                request.CustomerId,
                request.Currency,
                request.Country,
                request.SessionId,
                BasketStatus.FromName(request.Status.ToString())
            );

            foreach (var item in request.BasketItems)
            {
                var basketItem = BasketItem.Create(
                    basket.Id,
                    Guid.NewGuid(),
                    item.ProductCode,
                    item.Color,
                    item.Price,
                    item.Quantity,
                    BasketItemStatus.FromName(item.Status.ToString())
                );

                basket.AddItem(basketItem);
            }

            await basketRepository.AddAsync(basket, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return basket.Id;
        }
    }
}