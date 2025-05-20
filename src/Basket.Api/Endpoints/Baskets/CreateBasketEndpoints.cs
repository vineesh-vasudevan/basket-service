using Basket.Application.Basket.CreateBasket;

namespace Basket.Api.Endpoints.Baskets
{
    public class CreateBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/baskets", CreateBasket)
            .Produces<Guid>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Basket")
            .WithDescription("Create Basket");
        }

        private static async Task<IResult> CreateBasket(
           CreateBasketRequestDto request,
           [FromServices] IMapper mapper,
           [FromServices] ISender sender)
        {
            var command = new CreateBasketCommand(request);
            var result = await sender.Send(command);
            return Results.Created($"/baskets/{result}", result);
        }
    }
}