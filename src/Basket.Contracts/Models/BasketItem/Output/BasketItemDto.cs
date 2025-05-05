namespace Basket.Contracts.Models.BasketItem.Output
{
    public class BasketItemDto
    {
        public int Quantity { get; set; } = default!;
        public string Color { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string ProductCode { get; set; } = default!;
        public decimal TotalPrice { get; set; } = default!;
    }
}
