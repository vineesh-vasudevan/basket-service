namespace Basket.Domain.Exceptions
{
    public class BasketNotFoundException(Guid id) : NotFoundException($"Basket {id} was not found.")
    {
    }
}
