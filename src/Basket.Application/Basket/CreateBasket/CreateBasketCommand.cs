using Basket.Contracts.Models.Basket.Input;
using Basket.Shared.CQRS;

namespace Basket.Application.Basket.CreateBasket
{
    public record CreateBasketCommand(CreateBasketRequestDto CreateBasketRequest) : ICommand<Guid>;
}