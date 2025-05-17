using Basket.Domain.Entities;
using Basket.Domain.Enum;

namespace Basket.Mocks.Domain
{
    public class BasketBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Guid _customerId = Guid.NewGuid();
        private string _currency = "USD";
        private string _country = "US";
        private string _sessionId = Guid.NewGuid().ToString();
        private BasketStatus _status = BasketStatus.Active;
        private readonly List<BasketItem> _basketItems = new();

        public BasketBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public BasketBuilder WithCustomerId(Guid customerId)
        {
            _customerId = customerId;
            return this;
        }

        public BasketBuilder WithCurrency(string currency)
        {
            _currency = currency;
            return this;
        }

        public BasketBuilder WithCountry(string country)
        {
            _country = country;
            return this;
        }

        public BasketBuilder WithSessionId(string sessionId)
        {
            _sessionId = sessionId;
            return this;
        }

        public BasketBuilder WithStatus(BasketStatus status)
        {
            _status = status;
            return this;
        }

        public BasketBuilder AddBasketItem(BasketItem item)
        {
            _basketItems.Add(item);
            return this;
        }

        public BasketBuilder AddBasketItem(Action<BasketItemBuilder> configure)
        {
            var builder = new BasketItemBuilder().WithBasketId(_id);
            configure(builder);
            _basketItems.Add(builder.Build());
            return this;
        }

        public Basket.Domain.Entities.Basket Build()
        {
            var basket = Basket.Domain.Entities.Basket.Create(_id, _customerId, _currency, _country, _sessionId, _status);
            foreach (var item in _basketItems)
            {
                basket.AddItem(item);
            }
            return basket;
        }
    }
}