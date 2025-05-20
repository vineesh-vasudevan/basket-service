namespace Basket.Application.Basket.GetBasket
{
    public class GetBasketQueryHandler(IBasketRepository basketRepository, IMapper mapper) : IQueryHandler<GetBasketQuery, BasketDto>
    {
        public async Task<BasketDto> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(query.Id, cancellationToken);
            if (basket.HasNoValue)
            {
                throw new BasketNotFoundException(query.Id);
            }
            return mapper.Map<BasketDto>(basket.Value);
        }
    }
}