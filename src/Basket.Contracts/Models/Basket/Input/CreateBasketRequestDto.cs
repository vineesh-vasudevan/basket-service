using Basket.Contracts.Models.BasketItem.Input;
using Basket.Contracts.Models.Common;

namespace Basket.Contracts.Models.Basket.Input
{
    public class CreateBasketRequestDto
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public string Currency { get; init; }
        public string Country { get; init; }
        public string SessionId { get; init; } = default!;
        public BasketStatusDto Status { get; init; }
        public List<BasketItemCreateRequestDto> BasketItems { get; init; } = [];
    }
}