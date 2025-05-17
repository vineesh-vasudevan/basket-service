using FluentValidation;

namespace Basket.Application.CheckoutBasket
{
    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.Request).NotNull();

            When(x => x.Request is not null, () =>
            {
                RuleFor(x => x.Request.BasketId)
                    .NotEmpty().WithMessage("BasketId is required.");

                RuleFor(x => x.Request.CustomerId)
                    .NotEmpty().WithMessage("CustomerId is required.");

                RuleFor(x => x.Request.OrderName)
                    .NotEmpty().WithMessage("Order name is required.")
                    .MaximumLength(100);

                RuleFor(x => x.Request.ShippingAddress).SetValidator(new AddressDtoValidator());
                RuleFor(x => x.Request.BillingAddress).SetValidator(new AddressDtoValidator());
                RuleFor(x => x.Request.Payment).SetValidator(new PaymentDtoValidator());
            });
        }
    }
}