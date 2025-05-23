﻿namespace Basket.Application.BasketItems.UpdateBasketItem
{
    public record UpdateBasketItemCommand(Guid BasketId, Guid ItemId, int Quantity) : ICommand<BasketDto>;
}