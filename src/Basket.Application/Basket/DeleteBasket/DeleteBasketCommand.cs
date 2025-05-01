using Basket.Shared.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Basket.Application.Basket.DeleteBasket
{
    public record DeleteBasketCommand(Guid Id) : ICommand<bool>;
}
