using Basket.Contracts.Models.Basket.Output;
using Basket.Shared.CQRS;

namespace Basket.Application.Basket.GetBasket
{
    public record GetBasketQuery(Guid Id) : IQuery<BasketDto>;
}