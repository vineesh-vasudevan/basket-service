﻿namespace Basket.Application.Basket.DeleteBasket
{
    public record DeleteBasketCommand(Guid Id) : ICommand<bool>;
}