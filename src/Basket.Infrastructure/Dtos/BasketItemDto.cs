namespace Basket.Infrastructure.Dtos
{
    internal class BasketItemDto
    {
        public Guid Id { get; init; }
        public string ProductCode { get; init; } = string.Empty;
        public Guid BasketId { get; init; }
        public string Color { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public decimal TotalPrice { get; init; }
        public decimal Price { get; init; }
        public bool IsDeleted { get; init; }
    }
}
