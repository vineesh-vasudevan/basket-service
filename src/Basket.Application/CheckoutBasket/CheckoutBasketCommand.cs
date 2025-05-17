using Basket.Contracts.Dtos.BasketCheckout;
using Basket.Shared.CQRS;

namespace Basket.Application.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto Request) : ICommand<Guid>;
}