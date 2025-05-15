namespace Basket.Domain.Exceptions
{
    public class BasketItemNotFoundException(Guid id) : NotFoundException($"Basket Item {id} was not found.")
    {
    }
}