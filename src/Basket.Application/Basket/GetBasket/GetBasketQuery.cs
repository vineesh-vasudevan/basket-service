namespace Basket.Application.Basket.GetBasket
{
    public record GetBasketQuery(Guid Id) : IQuery<BasketDto>;
}