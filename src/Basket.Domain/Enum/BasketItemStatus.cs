using Ardalis.SmartEnum;

namespace Basket.Domain.Enum
{
    public class BasketItemStatus(string name, int value) : SmartEnum<BasketItemStatus>(name, value)
    {
        public static readonly BasketItemStatus Active = new(nameof(Active), 0);
        public static readonly BasketItemStatus Cancelled = new(nameof(Cancelled), 1);
        public static readonly BasketItemStatus CheckOut = new(nameof(CheckOut), 2);
    }
}