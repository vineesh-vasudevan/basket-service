namespace Basket.Application.Basket.CreateBasket
{
    public record CreateBasketCommand(CreateBasketRequestDto CreateBasketRequest) : ICommand<Guid>;
}