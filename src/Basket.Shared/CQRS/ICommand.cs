using MediatR;

namespace Basket.Shared.CQRS
{
    public interface ICommand : ICommand<Unit>
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}