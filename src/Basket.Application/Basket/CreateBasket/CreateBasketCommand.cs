using Basket.Contracts.Dtos.Basket.Input;
using Basket.Shared.CQRS;

namespace Basket.Application.Basket.CreateBasket
{
    public record CreateBasketCommand(CreateBasketRequestDto CreateBasketRequest) : ICommand<Guid>;
}