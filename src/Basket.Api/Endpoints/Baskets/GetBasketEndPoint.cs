using Basket.Application.Basket.GetBasket;
using Basket.Contracts.Dtos.Basket.Output;

namespace Basket.Api.Endpoints.Baskets
{
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/baskets/{id}", GetBasket)
                .WithName("GetBasket")
                .WithSummary("Get Basket By Id")
                .WithDescription("Get Basket By Id")
                .Produces<BasketDto>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }

        private static async Task<IResult> GetBasket(Guid id, ISender sender)
        {
            var result = await sender.Send(new GetBasketQuery(id));
            return Results.Ok(result);
        }
    }
}