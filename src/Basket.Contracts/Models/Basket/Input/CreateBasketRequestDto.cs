using Basket.Contracts.Models.BasketItem.Input;

namespace Basket.Contracts.Models.Basket.Input
{
    public class CreateBasketRequestDto
    {
        public Guid UserId { get; init; }
        public required string Currency { get; init; }
        public required string Country { get; init; }
        public string SessionId { get; init; } = default!;
        public List<BasketItemCreateRequestDto> BasketItems { get; init; } = [];
    }
}
