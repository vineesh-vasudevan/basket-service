namespace Basket.Contracts.Models.BasketItem.Input
{
    public class BasketItemCreateRequestDto
    {
        public Guid ProductId { get; init; }
        public int Quantity { get; init; }
        public string Color { get; init; } = default!;
        public decimal Price { get; init; }
        public string ProductName { get; init; } = default!;
    }
}
