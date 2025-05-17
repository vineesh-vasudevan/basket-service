using AutoMapper;
using Basket.Contracts.Events;
using Basket.Domain.Exceptions;
using Basket.Domain.Messaging;
using Basket.Domain.Repositories;
using Basket.Shared.CQRS;

namespace Basket.Application.CheckoutBasket
{
    public class CheckoutBasketCommandHandler(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper mapper, IBasketEventPublisher basketEventPublisher)
        : ICommandHandler<CheckoutBasketCommand, Guid>
    {
        public async Task<Guid> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            var maybeBasket = await basketRepository.GetBasket(command.Request.BasketId, cancellationToken);
            if (maybeBasket.HasNoValue)
            {
                throw new BasketNotFoundException(command.Request.BasketId);
            }

            var basket = maybeBasket.Value;
            basket.Checkout();

            var basketCheckoutEvent = mapper.Map<BasketCheckoutEvent>((basket, command.Request));
            await basketEventPublisher.PublishAsync(basketCheckoutEvent, cancellationToken);
            basketRepository.Update(basket);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return basketCheckoutEvent.Id;
        }
    }
}