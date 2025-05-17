using Basket.Contracts.Dtos.Common;

namespace Basket.Contracts.Dtos.BasketItem.Input
{
    public class BasketItemCreateRequestDto
    {
        public string ProductCode { get; init; } = default!;
        public int Quantity { get; init; }
        public string Color { get; init; } = default!;
        public decimal Price { get; init; }
        public BasketItemStatusDto Status { get; init; }
    }
}