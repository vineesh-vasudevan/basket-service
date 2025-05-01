
using Basket.Application.BasketItems.UpdateBasketItem;
using Basket.Contracts.Models.BasketItem.Input;
using Basket.Contracts.Models.BasketItem.Output;
using Basket.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Endpoints.BasketItems
{
    public class PatchBasketItemEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapMethods("/baskets/{basketId:guid}/items/{itemId:guid}", ["PATCH"], PatchBasketItem)
               .Produces<BasketItemDto>(StatusCodes.Status204NoContent)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .ProducesProblem(StatusCodes.Status404NotFound)
               .WithSummary("Update Basket Item")
               .WithDescription("Updates the quantity of a basket item.");
        }

        private static async Task<IResult> PatchBasketItem(
        [FromRoute] Guid basketId,
        [FromRoute] Guid itemId,
        [FromBody] BasketItemPatchRequestDto request,
        [FromServices] ISender sender)
        {
            var command = new UpdateBasketItemCommand(basketId, itemId, request.Quantity);
            var result = await sender.Send(command);
            return Results.Ok(result);
        }
    }
}
