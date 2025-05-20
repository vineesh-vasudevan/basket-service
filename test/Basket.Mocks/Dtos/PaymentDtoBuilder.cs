using Basket.Contracts.Dtos.BasketCheckout;

namespace Basket.Mocks.Dtos
{
    public class PaymentDtoBuilder
    {
        private decimal _amount = 100.00m;
        private string _currency = "USD";
        private DateTime _paidAt = DateTime.UtcNow;
        private string _paymentMethod = "CreditCard";
        private bool _isSuccessful = true;
        private string _transactionId = Guid.NewGuid().ToString();

        public PaymentDtoBuilder WithAmount(decimal amount)
        { _amount = amount; return this; }

        public PaymentDtoBuilder WithCurrency(string currency)
        { _currency = currency; return this; }

        public PaymentDtoBuilder WithPaidAt(DateTime paidAt)
        { _paidAt = paidAt; return this; }

        public PaymentDtoBuilder WithPaymentMethod(string method)
        { _paymentMethod = method; return this; }

        public PaymentDtoBuilder WithIsSuccessful(bool isSuccess)
        { _isSuccessful = isSuccess; return this; }

        public PaymentDtoBuilder WithTransactionId(string transactionId)
        { _transactionId = transactionId; return this; }

        public PaymentDto Build()
        {
            return new PaymentDto
            {
                Amount = _amount,
                Currency = _currency,
                PaidAt = _paidAt,
                PaymentMethod = _paymentMethod,
                IsSuccessful = _isSuccessful,
                TransactionId = _transactionId
            };
        }

        public static PaymentDto Default() => new PaymentDtoBuilder().Build();
    }
}