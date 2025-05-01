using FluentValidation;
using Basket.Contracts.Models.BasketItem.Input;

namespace Basket.Application.Basket.CreateBasket
{
    public class BasketItemCreateRequestDtoValidator : AbstractValidator<BasketItemCreateRequestDto>
    {
        public BasketItemCreateRequestDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("ProductName is required.")
                .MaximumLength(200).WithMessage("ProductName must not exceed 200 characters.");

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("Color is required.")
                .MaximumLength(100).WithMessage("Color must not exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}
