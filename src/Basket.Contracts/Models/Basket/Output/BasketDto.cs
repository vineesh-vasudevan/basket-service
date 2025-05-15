using Basket.Contracts.Models.BasketItem.Output;
using Basket.Contracts.Models.Common;

namespace Basket.Contracts.Models.Basket.Output
{
    public class BasketDto
    {
        public Guid Id { get; init; } = default!;
        public Guid CustomerId { get; init; } = default!;
        public List<BasketItemDto> Items { get; init; } = new();
        public decimal TotalPrice { get; init; } = default!;
        public string Currency { get; init; } = default!;
        public string Country { get; init; } = default!;
        public string SessionId { get; init; } = default!;
        public BasketStatusDto Status { get; init; } = default!;
    }
}