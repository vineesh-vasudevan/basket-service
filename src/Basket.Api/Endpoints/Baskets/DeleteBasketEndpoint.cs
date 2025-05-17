using Basket.Application.Basket.DeleteBasket;
using Basket.Contracts.Dtos.Basket.Output;

namespace Basket.Api.Endpoints.Baskets
{
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/baskets/{id}", DeleteBasket)
                .WithSummary("Delete Basket")
                .WithDescription("Delete Basket By Id")
                .Produces<BasketDto>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        private static async Task<IResult> DeleteBasket(Guid id, ISender sender)
        {
            var result = await sender.Send(new DeleteBasketCommand(id));
            return Results.NoContent();
        }
    }
}