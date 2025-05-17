namespace Basket.Contracts.Dtos.BasketCheckout
{
    public class AddressDto
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string Street { get; init; } = default!;
        public string City { get; init; } = default!;
        public string State { get; init; } = default!;
        public string PostalCode { get; init; } = default!;
        public string Country { get; init; } = default!;
    }
}