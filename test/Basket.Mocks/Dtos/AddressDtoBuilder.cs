using Basket.Contracts.Dtos.BasketCheckout;

namespace Basket.Mocks.Dtos
{
    public class AddressDtoBuilder
    {
        private string _firstName = "John";
        private string _lastName = "Doe";
        private string _street = "123 Main St";
        private string _city = "New York";
        private string _state = "NY";
        private string _postalCode = "10001";
        private string _country = "USA";

        public AddressDtoBuilder WithFirstName(string firstName)
        { _firstName = firstName; return this; }

        public AddressDtoBuilder WithLastName(string lastName)
        { _lastName = lastName; return this; }

        public AddressDtoBuilder WithStreet(string street)
        { _street = street; return this; }

        public AddressDtoBuilder WithCity(string city)
        { _city = city; return this; }

        public AddressDtoBuilder WithState(string state)
        { _state = state; return this; }

        public AddressDtoBuilder WithPostalCode(string code)
        { _postalCode = code; return this; }

        public AddressDtoBuilder WithCountry(string country)
        { _country = country; return this; }

        public AddressDto Build()
        {
            return new AddressDto
            {
                FirstName = _firstName,
                LastName = _lastName,
                Street = _street,
                City = _city,
                State = _state,
                PostalCode = _postalCode,
                Country = _country
            };
        }

        public static AddressDto Default() => new AddressDtoBuilder().Build();
    }
}