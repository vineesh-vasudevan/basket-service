namespace Basket.Infrastructure.Dtos
{
    internal class BasketItemCacheDto
    {
        public Guid Id { get; init; }
        public string ProductCode { get; init; } = string.Empty;
        public Guid BasketId { get; init; }
        public string Color { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public BasketItemStatusCacheDto Status { get; init; }

        private decimal _price;
        private decimal _totalPrice;

        public decimal Price
        {
            get => Math.Round(_price, 2);
            init => _price = value;
        }

        public decimal TotalPrice
        {
            get => Math.Round(_totalPrice, 2);
            init => _totalPrice = value;
        }
    }
}