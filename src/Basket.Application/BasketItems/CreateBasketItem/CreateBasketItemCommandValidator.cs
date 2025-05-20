namespace Basket.Application.BasketItems.CreateBasketItem
{
    public class CreateBasketItemCommandValidator : AbstractValidator<CreateBasketItemCommand>
    {
        public CreateBasketItemCommandValidator()
        {
            RuleFor(x => x.BasketId)
                .NotEmpty().WithMessage("BasketId is required.");

            RuleFor(x => x.Request)
                .NotNull().WithMessage("Basket item is required.");

            When(x => x.Request != null, () =>
            {
                RuleFor(x => x.Request.ProductCode)
                .NotEmpty().WithMessage("ProductCode is required.");

                RuleFor(x => x.Request.Color)
                    .NotEmpty().WithMessage("Color is required.")
                    .MaximumLength(100).WithMessage("Color must not exceed 100 characters.");

                RuleFor(x => x.Request.Price)
                    .GreaterThan(0).WithMessage("Price must be greater than zero.");

                RuleFor(x => x.Request.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
            });
        }
    }
}