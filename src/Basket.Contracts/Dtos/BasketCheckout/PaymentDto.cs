namespace Basket.Contracts.Dtos.BasketCheckout
{
    public class PaymentDto
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; } = default!;
        public DateTime PaidAt { get; set; }
        public string PaymentMethod { get; set; } = default!;
        public bool IsSuccessful { get; set; }
        public string TransactionId { get; set; } = default!;
    }
}