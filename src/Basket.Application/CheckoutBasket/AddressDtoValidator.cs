using Basket.Contracts.Dtos.BasketCheckout;
using FluentValidation;

namespace Basket.Application.CheckoutBasket
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
        }
    }
}