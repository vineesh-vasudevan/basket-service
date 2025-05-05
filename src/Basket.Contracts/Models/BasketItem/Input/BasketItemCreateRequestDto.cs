namespace Basket.Contracts.Models.BasketItem.Input
{
    public class BasketItemCreateRequestDto
    {
        public string ProductCode { get; init; } = default!;
        public int Quantity { get; init; }
        public string Color { get; init; } = default!;
        public decimal Price { get; init; }
    }
}
