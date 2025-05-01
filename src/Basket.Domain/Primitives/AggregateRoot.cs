using CSharpFunctionalExtensions;

namespace Basket.Domain.Primitives
{
    public abstract class AggregateRoot(Guid guid) : Entity<Guid>(guid)
    {
    }
}
