using Basket.Contracts.Dtos.BasketItem.Input;
using Basket.Contracts.Dtos.Common;

namespace Basket.Contracts.Dtos.Basket.Input
{
    public class CreateBasketRequestDto
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public string Currency { get; init; } = default!;
        public string Country { get; init; } = default!;
        public string SessionId { get; init; } = default!;
        public BasketStatusDto Status { get; init; }
        public List<BasketItemCreateRequestDto> BasketItems { get; init; } = [];
    }
}