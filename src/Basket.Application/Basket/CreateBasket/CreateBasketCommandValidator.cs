using FluentValidation;

namespace Basket.Application.Basket.CreateBasket
{
    internal class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
    {
        public CreateBasketCommandValidator()
        {
            RuleFor(x => x.CreateBasketRequest.Id)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.CreateBasketRequest.Currency)
                .NotEmpty().WithMessage("Currency is required.");

            RuleFor(x => x.CreateBasketRequest.Country)
                .NotEmpty().WithMessage("Country is required.");

            RuleFor(x => x.CreateBasketRequest.SessionId)
                .NotEmpty().WithMessage("SessionId is required.");

            RuleFor(x => x.CreateBasketRequest.BasketItems)
                .NotNull().WithMessage("Basket items are required.")
                .Must(x => x.Count > 0).WithMessage("At least one basket item is required.");

            RuleForEach(x => x.CreateBasketRequest.BasketItems)
                .SetValidator(new BasketItemCreateRequestDtoValidator());
        }
    }
}