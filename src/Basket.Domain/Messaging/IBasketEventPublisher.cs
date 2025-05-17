namespace Basket.Domain.Messaging
{
    public interface IBasketEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken)
            where TEvent : class;
    }
}