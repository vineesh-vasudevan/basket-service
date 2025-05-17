using Basket.Contracts.Dtos.BasketCheckout;
using Basket.Contracts.Dtos.Common;

namespace Basket.Contracts.Events
{
    public class BasketCheckoutEvent
    {
        public Guid Id { get; init; }
        public Guid BasketId { get; init; }
        public Guid CustomerId { get; init; }
        public string OrderName { get; init; } = default!;
        public BasketStatusDto Status { get; private init; }

        public List<BasketItemEvent> Items { get; init; } = [];

        public AddressDto ShippingAddress { get; init; } = default!;
        public AddressDto BillingAddress { get; init; } = default!;
        public PaymentDto Payment { get; init; } = default!;
        public DateTime CheckedOutAt { get; init; } = DateTime.UtcNow;
    }
}