namespace Basket.Infrastructure.Dtos
{
    internal class BasketCacheDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public string Currency { get; init; } = default!;
        public string Country { get; init; } = default!;
        public string SessionId { get; init; } = default!;
        public bool IsDeleted { get; init; }
        public BasketStatusCacheDto Status { get; set; }
        public List<BasketItemCacheDto> BasketItems { get; init; } = new();
    }
}