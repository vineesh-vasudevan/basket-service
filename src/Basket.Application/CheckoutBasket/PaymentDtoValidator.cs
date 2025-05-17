using Basket.Contracts.Dtos.BasketCheckout;
using FluentValidation;

namespace Basket.Application.CheckoutBasket
{
    public class PaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        public PaymentDtoValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Payment amount must be greater than zero.");

            RuleFor(x => x.Currency)
                .NotNull().WithMessage("Currency must be provided.");

            RuleFor(x => x.PaidAt)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("PaidAt cannot be in the future.");

            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Payment method is required.")
                .MaximumLength(50);

            RuleFor(x => x.TransactionId)
                .NotNull().WithMessage("Transaction ID is required.")
                .Must(t => !string.IsNullOrWhiteSpace(t))
                .WithMessage("Transaction ID value cannot be empty.");
        }
    }
}