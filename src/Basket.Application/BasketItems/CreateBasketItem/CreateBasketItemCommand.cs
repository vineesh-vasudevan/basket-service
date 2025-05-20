namespace Basket.Application.BasketItems.CreateBasketItem
{
    public record CreateBasketItemCommand(BasketItemCreateRequestDto Request, Guid BasketId) : ICommand<Guid>;
}