using CSharpFunctionalExtensions;

namespace Basket.Domain.Entities
{
    public class BasketItem : Entity<Guid>
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string Color { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public Guid BasketId { get; private set; }
        public bool IsDeleted { get; private set; }

        public decimal TotalPrice { get; private set; }

        private void RecalculateTotal() => TotalPrice = Price * Quantity;

        private BasketItem(Guid basketId, Guid id, Guid productId, string productName, string color, decimal price, int quantity)
        : base(id)
        {
            BasketId = basketId;
            ProductId = productId;
            ProductName = productName;
            Color = color;
            Price = price;
            Quantity = quantity;
            RecalculateTotal();
        }

        public static BasketItem Create(Guid basketId, Guid id, Guid productId, string productName, string color, decimal price, int quantity)
        {
            if (productId == Guid.Empty) throw new ArgumentException("ProductId cannot be empty.", nameof(productId));
            if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentException("ProductName cannot be empty.", nameof(productName));
            if (string.IsNullOrWhiteSpace(color)) throw new ArgumentException("Color cannot be empty.", nameof(color));
            if (price <= 0) throw new ArgumentException("Price must be greater than zero.", nameof(price));
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

            return new BasketItem(basketId, id, productId, productName, color, price, quantity);
        }

        public void AddQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            Quantity += quantity;
            RecalculateTotal();
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative.");

            Quantity = quantity;
            RecalculateTotal();
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
