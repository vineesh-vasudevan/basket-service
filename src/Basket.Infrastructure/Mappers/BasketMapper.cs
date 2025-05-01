using Basket.Domain.Entities;
using Basket.Infrastructure.Dtos;

namespace Basket.Infrastructure.Mappers
{
    internal class BasketMapper
    {
        public static BasketDto ToDto(Domain.Entities.Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                UserId = basket.UserId,
                Currency = basket.Currency,
                Country = basket.Country,
                SessionId = basket.SessionId,
                IsDeleted = basket.IsDeleted,
                BasketItems = basket.BasketItems.Select(item => new BasketItemDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Color = item.Color,
                    Quantity = item.Quantity,
                    TotalPrice = item.TotalPrice,
                    IsDeleted = item.IsDeleted,
                    Price = item.Price,
                    BasketId = item.Id,
                    ProductName = item.ProductName,                    
                }).ToList()
            };
        }

        public static Domain.Entities.Basket FromDto(BasketDto dto)
        {
            var basket = Domain.Entities.Basket.Create(dto.Id, dto.UserId, dto.Currency, dto.Country, dto.SessionId);

            foreach (var item in dto.BasketItems)
            {
                var basketItem = BasketItem.Create(basket.Id, item.Id, item.ProductId, item.ProductName, item.Color, item.Price, item.Quantity);
                basket.AddItem(basketItem);
            }

            if (dto.IsDeleted) basket.Delete();

            return basket;
        }

    }
}
