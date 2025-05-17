using Basket.Domain.Exceptions;

namespace Basket.Domain.ValueObjects
{
    public readonly record struct TransactionId
    {
        public string Value { get; }

        public TransactionId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("TransactionId cannot be empty.");

            Value = value.Trim();
        }

        public static TransactionId Of(string value) => new(value);

        public override string ToString() => Value;
    }
}