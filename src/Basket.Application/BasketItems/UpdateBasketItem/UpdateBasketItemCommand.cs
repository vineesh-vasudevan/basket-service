using Basket.Contracts.Dtos.Basket.Output;
using Basket.Shared.CQRS;

namespace Basket.Application.BasketItems.UpdateBasketItem
{
    public record UpdateBasketItemCommand(Guid BasketId, Guid ItemId, int Quantity) : ICommand<BasketDto>;
}