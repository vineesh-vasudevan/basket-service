using Basket.Application.BasketItems.CreateBasketItem;

namespace Basket.Api.Endpoints.BasketItems
{
    public class CreateBasketItemEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/baskets/{basketId:guid}/items", CreateBasketItem)
               .Produces<Guid>(StatusCodes.Status201Created)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Create Basket Item")
               .WithDescription("Adds an item to the specified basket");
        }

        private static async Task<IResult> CreateBasketItem(
        [FromRoute] Guid basketId,
        BasketItemCreateRequestDto request,
        [FromServices] ISender sender)
        {
            var command = new CreateBasketItemCommand(request, basketId);
            var result = await sender.Send(command);
            return Results.Created($"/baskets/{basketId}/items/{result}", result);
        }
    }
}