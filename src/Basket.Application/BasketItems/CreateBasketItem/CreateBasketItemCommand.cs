using Basket.Contracts.Models.BasketItem.Input;
using Basket.Shared.CQRS;

namespace Basket.Application.BasketItems.CreateBasketItem
{
    public record CreateBasketItemCommand(BasketItemCreateRequestDto Request, Guid BasketId) : ICommand<Guid>;
}
