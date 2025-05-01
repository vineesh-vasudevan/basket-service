using Basket.Domain.Exceptions;
using Basket.Domain.Repositories;
using Basket.Shared.CQRS;

namespace Basket.Application.Basket.DeleteBasket
{
    public class DeleteBasketCommandHandler(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        : ICommandHandler<DeleteBasketCommand, bool>
    {
        public async Task<bool> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await basketRepository.GetBasket(command.Id, cancellationToken);

            if (basket.HasNoValue)
            {
                throw new BasketNotFoundException(command.Id);
            }
            basket.Value.Delete();

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                basketRepository.Update(basket.Value);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                await unitOfWork.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
