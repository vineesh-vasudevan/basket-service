namespace Basket.Infrastructure.Dtos
{
    internal class BasketDto
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public string Currency { get; init; } = default!;
        public string Country { get; init; } = default!;
        public string SessionId { get; init; } = default!;
        public bool IsDeleted { get; init; }
        public List<BasketItemDto> BasketItems { get; init; } = new();
    }
}
