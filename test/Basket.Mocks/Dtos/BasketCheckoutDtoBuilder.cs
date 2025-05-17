using Basket.Contracts.Dtos.BasketCheckout;

namespace Basket.Mocks.Dtos
{
    public class BasketCheckoutDtoBuilder
    {
        private Guid _basketId = Guid.NewGuid();
        private Guid _customerId = Guid.NewGuid();
        private string _orderName = "Default Order";
        private AddressDto _shippingAddress = new AddressDtoBuilder().Build();
        private AddressDto _billingAddress = new AddressDtoBuilder().Build();
        private PaymentDto _payment = new PaymentDtoBuilder().Build();

        public BasketCheckoutDtoBuilder WithBasketId(Guid basketId)
        {
            _basketId = basketId;
            return this;
        }

        public BasketCheckoutDtoBuilder WithCustomerId(Guid customerId)
        {
            _customerId = customerId;
            return this;
        }

        public BasketCheckoutDtoBuilder WithOrderName(string orderName)
        {
            _orderName = orderName;
            return this;
        }

        public BasketCheckoutDtoBuilder WithShippingAddress(AddressDto address)
        {
            _shippingAddress = address;
            return this;
        }

        public BasketCheckoutDtoBuilder WithBillingAddress(AddressDto address)
        {
            _billingAddress = address;
            return this;
        }

        public BasketCheckoutDtoBuilder WithPayment(PaymentDto payment)
        {
            _payment = payment;
            return this;
        }

        public BasketCheckoutDto Build()
        {
            return new BasketCheckoutDto
            {
                BasketId = _basketId,
                CustomerId = _customerId,
                OrderName = _orderName,
                ShippingAddress = _shippingAddress,
                BillingAddress = _billingAddress,
                Payment = _payment
            };
        }
    }
}