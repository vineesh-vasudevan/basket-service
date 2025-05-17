namespace Basket.Contracts.Dtos.BasketCheckout
{
    public class BasketCheckoutDto
    {
        public Guid BasketId { get; set; }
        public Guid CustomerId { get; init; }
        public string OrderName { get; init; } = default!;
        public AddressDto ShippingAddress { get; init; } = default!;
        public AddressDto BillingAddress { get; init; } = default!;
        public PaymentDto Payment { get; init; } = default!;
    }
}