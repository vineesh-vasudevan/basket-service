using Basket.Domain.Exceptions;

namespace Basket.Domain.ValueObjects
{
    public readonly record struct BasketId
    {
        public Guid Value { get; }

        public BasketId(Guid value)
        {
            if (value == Guid.Empty)
                throw new DomainException("BasketId cannot be empty.");

            Value = value;
        }

        public static BasketId New() => new(Guid.NewGuid());

        public static BasketId Of(Guid value) => new(value);

        public override string ToString() => Value.ToString();
    }
}