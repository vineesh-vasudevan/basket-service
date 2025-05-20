namespace Basket.Application.BasketItems.RemoveBasketItem
{
    public record RemoveBasketItemCommand(Guid BasketId, Guid ItemId) : ICommand<BasketDto>;
}