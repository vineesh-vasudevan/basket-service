using Basket.Contracts.Dtos.Common;

namespace Basket.Contracts.Events
{
    public class BasketItemEvent
    {
        public Guid Id { get; init; }
        public string ProductCode { get; init; } = default!;
        public string Color { get; init; } = default!;
        public decimal Price { get; init; }
        public int Quantity { get; init; }
        public Guid BasketId { get; init; }
        public BasketItemStatusDto Status { get; init; }
        public decimal TotalPrice { get; init; }
    }
}