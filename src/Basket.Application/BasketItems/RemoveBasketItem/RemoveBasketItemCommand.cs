using Basket.Contracts.Dtos.Basket.Output;
using Basket.Shared.CQRS;

namespace Basket.Application.BasketItems.RemoveBasketItem
{
    public record RemoveBasketItemCommand(Guid BasketId, Guid ItemId) : ICommand<BasketDto>;
}