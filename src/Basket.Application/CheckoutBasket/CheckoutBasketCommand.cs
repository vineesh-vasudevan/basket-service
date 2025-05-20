namespace Basket.Application.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto Request) : ICommand<Guid>;
}