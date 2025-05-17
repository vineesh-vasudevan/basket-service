using Basket.Contracts.Dtos.Common;

namespace Basket.Contracts.Dtos.BasketItem.Output
{
    public class BasketItemDto
    {
        public Guid Id { get; set; } = default!;
        public Guid BasketId { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public string Color { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string ProductCode { get; set; } = default!;
        public decimal TotalPrice { get; set; } = default!;
        public BasketItemStatusDto Status { get; init; }
    }
}