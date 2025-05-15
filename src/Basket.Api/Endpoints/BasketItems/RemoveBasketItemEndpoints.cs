using Basket.Application.BasketItems.RemoveBasketItem;
using Basket.Contracts.Models.BasketItem.Output;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Endpoints.BasketItems
{
    public class RemoveBasketItemEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/baskets/{basketId:guid}/items/{itemId:guid}", RemoveBasketItem)
                .Produces<BasketItemDto>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Remove Basket Item")
                .WithDescription("Removes an item from the specified basket");
        }

        private static async Task<IResult> RemoveBasketItem(
        [FromRoute] Guid basketId,
        [FromRoute] Guid itemId,
        [FromServices] ISender sender)
        {
            var command = new RemoveBasketItemCommand(basketId, itemId);
            var result = await sender.Send(command);
            return Results.Ok(result);
        }
    }
}