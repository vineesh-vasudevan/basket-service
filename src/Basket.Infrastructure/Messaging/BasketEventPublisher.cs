using Basket.Domain.Messaging;
using MassTransit;

namespace Basket.Infrastructure.Messaging
{
    public class BasketEventPublisher(IPublishEndpoint publishEndpoint) : IBasketEventPublisher
    {
        public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken) where TEvent : class
        {
            return publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}