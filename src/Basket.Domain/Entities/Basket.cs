using Basket.Domain.Enum;
using Basket.Domain.Exceptions;
using Basket.Domain.Primitives;
using CSharpFunctionalExtensions;

namespace Basket.Domain.Entities
{
    public class Basket : AggregateRoot
    {
        private readonly List<BasketItem> _items = [];

        public Guid CustomerId { get; private set; }
        public string Currency { get; private set; }
        public string Country { get; private set; }
        public string SessionId { get; private set; }

        public BasketStatus Status { get; private set; }

        public decimal TotalPrice { get; private set; }

        public IReadOnlyCollection<BasketItem> BasketItems => _items.AsReadOnly();

        private Basket(Guid id, Guid customerId, string currency, string country, string sessionId, BasketStatus status)
            : base(id)
        {
            if (customerId == Guid.Empty) throw new ArgumentException("CustomerId cannot be empty.", nameof(customerId));
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency cannot be empty.", nameof(currency));
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country cannot be empty.", nameof(country));
            if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("SessionId cannot be empty.", nameof(sessionId));

            CustomerId = customerId;
            Currency = currency;
            Country = country;
            SessionId = sessionId;
            RecalculateTotal();
            Status = status;
        }

        public static Basket Create(Guid id, Guid customerId, string currency, string country, string sessionId, BasketStatus status)
        {
            return new Basket(id, customerId, currency, country, sessionId, status);
        }

        public void AddItem(BasketItem basketItem)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductCode == basketItem.ProductCode && i.Color == basketItem.Color);
            if (existingItem != null)
            {
                existingItem.AddQuantity(basketItem.Quantity);
            }
            else
            {
                _items.Add(basketItem);
            }
            RecalculateTotal();
        }

        public void RemoveItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                _items.Remove(item);
            }
            RecalculateTotal();
        }

        public void UpdateItem(Guid itemId, int quantity)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);
            item?.UpdateQuantity(quantity);
            RecalculateTotal();
        }

        public void Delete()
        {
            Status = BasketStatus.Cancelled;
            foreach (var item in _items)
            {
                item.Delete();
            }
            RecalculateTotal();
        }

        public void DeleteBasketItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item is null)
            {
                throw new BasketItemNotFoundException(itemId);
            }

            item.Delete();
            RecalculateTotal();
        }

        public Maybe<BasketItem> GetBasketItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);
            return Maybe.From(item);
        }

        private void RecalculateTotal() => TotalPrice = _items
            .Where(i => i.Status != BasketItemStatus.Cancelled)
            .Sum(i => i.TotalPrice);

        public void Checkout()
        {
            Status = BasketStatus.CheckedOut;
            foreach (var item in _items)
            {
                item.Checkout();
            }
        }
    }
}