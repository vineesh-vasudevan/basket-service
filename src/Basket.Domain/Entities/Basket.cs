using Basket.Domain.Exceptions;
using Basket.Domain.Primitives;
using CSharpFunctionalExtensions;

namespace Basket.Domain.Entities
{
    public class Basket : AggregateRoot
    {
        private readonly List<BasketItem> _items = [];

        public Guid UserId { get; private set; }
        public string Currency { get; private set; }
        public string Country { get; private set; }
        public string SessionId { get; private set; }

        public decimal TotalPrice { get; private set; }

        public IReadOnlyCollection<BasketItem> BasketItems => _items.AsReadOnly();

        private Basket(Guid id, Guid userId, string currency, string country, string sessionId)
            : base(id)
        {
            if (userId == Guid.Empty) throw new ArgumentException("UserId cannot be empty.", nameof(userId));
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency cannot be empty.", nameof(currency));
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country cannot be empty.", nameof(country));
            if (string.IsNullOrWhiteSpace(sessionId)) throw new ArgumentException("SessionId cannot be empty.", nameof(sessionId));

            UserId = userId;
            Currency = currency;
            Country = country;
            SessionId = sessionId;
            RecalculateTotal();
        }

        public static Basket Create(Guid id, Guid userId, string currency, string country, string sessionId)
        {
            return new Basket(id, userId, currency, country, sessionId);
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

        public bool IsDeleted { get; private set; }

        public void Delete()
        {
            IsDeleted = true;
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

        private void RecalculateTotal() => TotalPrice = _items.Where(i => !i.IsDeleted).Sum(i => i.TotalPrice);        
    }
}
