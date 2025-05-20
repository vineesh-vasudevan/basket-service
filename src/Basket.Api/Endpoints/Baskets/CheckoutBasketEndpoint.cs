using Basket.Application.CheckoutBasket;

namespace Basket.Api.Endpoints.Baskets
{
    public class CheckoutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/baskets/checkout", CheckoutBasket)
                .Produces<Guid>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Checkout Basket")
                .WithDescription("Checkout Basket");
        }

        private static async Task<IResult> CheckoutBasket(
           BasketCheckoutDto request,
           [FromServices] ISender sender)
        {
            var command = new CheckoutBasketCommand(request);
            var result = await sender.Send(command);
            return Results.Created($"/baskets/checkout/{result}", result);
        }
    }
}