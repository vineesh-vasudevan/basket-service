using Basket.Domain.Entities;
using Basket.Domain.Enum;

namespace Basket.Mocks.Domain
{
    public class BasketItemBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Guid _basketId = Guid.NewGuid();
        private string _productCode = "DEFAULT_PRODUCT";
        private string _color = "Red";
        private decimal _price = 10.0m;
        private int _quantity = 1;
        private BasketItemStatus _status = BasketItemStatus.Active;

        public BasketItemBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public BasketItemBuilder WithBasketId(Guid basketId)
        {
            _basketId = basketId;
            return this;
        }

        public BasketItemBuilder WithProductCode(string productCode)
        {
            _productCode = productCode;
            return this;
        }

        public BasketItemBuilder WithColor(string color)
        {
            _color = color;
            return this;
        }

        public BasketItemBuilder WithPrice(decimal price)
        {
            _price = price;
            return this;
        }

        public BasketItemBuilder WithQuantity(int quantity)
        {
            _quantity = quantity;
            return this;
        }

        public BasketItemBuilder WithStatus(BasketItemStatus status)
        {
            _status = status;
            return this;
        }

        public BasketItem Build()
        {
            return BasketItem.Create(_basketId, _id, _productCode, _color, _price, _quantity, _status);
        }
    }
}