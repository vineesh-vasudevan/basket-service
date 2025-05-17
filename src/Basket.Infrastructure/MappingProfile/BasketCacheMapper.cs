using Basket.Domain.Entities;
using Basket.Domain.Enum;
using Basket.Infrastructure.Dtos;

namespace Basket.Infrastructure.MappingProfile
{
    internal class BasketCacheMapper
    {
        public static BasketCacheDto ToDto(Domain.Entities.Basket basket)
        {
            return new BasketCacheDto
            {
                Id = basket.Id,
                UserId = basket.CustomerId,
                Currency = basket.Currency,
                Country = basket.Country,
                SessionId = basket.SessionId,
                Status = (BasketStatusCacheDto)basket.Status.Value,
                BasketItems = basket.BasketItems.Select(item => new BasketItemCacheDto
                {
                    Id = item.Id,
                    ProductCode = item.ProductCode,
                    Color = item.Color,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                    Status = (BasketItemStatusCacheDto)item.Status.Value,
                    Price = item.Price,
                    BasketId = item.Id,
                }).ToList()
            };
        }

        public static Domain.Entities.Basket FromDto(BasketCacheDto dto)
        {
            var basketStatus = BasketStatus.FromName(dto.Status.ToString());
            var basket = Domain.Entities.Basket.Create(dto.Id, dto.UserId, dto.Currency, dto.Country, dto.SessionId, basketStatus);

            foreach (var item in dto.BasketItems)
            {
                var basketItemStatus = BasketItemStatus.FromName(item.Status.ToString());
                var basketItem = BasketItem.Create(basket.Id, item.Id, item.ProductCode, item.Color, item.Price, item.Quantity, basketItemStatus);
                basket.AddItem(basketItem);
            }

            return basket;
        }
    }
}