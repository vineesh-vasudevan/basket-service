using Ardalis.SmartEnum;

namespace Basket.Domain.Enum
{
    public class BasketStatus(string name, int value) : SmartEnum<BasketStatus>(name, value)
    {
        public static readonly BasketStatus Active = new(nameof(Active), 0);
        public static readonly BasketStatus Cancelled = new(nameof(Cancelled), 1);
        public static readonly BasketStatus CheckOut = new(nameof(CheckOut), 1);
    }
}