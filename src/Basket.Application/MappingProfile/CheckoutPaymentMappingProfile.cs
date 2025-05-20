using AutoMapper;
using Basket.CheckOutEvent;
using Basket.Contracts.Dtos.BasketCheckout;

namespace Basket.Application.MappingProfile
{
    public class CheckoutPaymentMappingProfile : Profile
    {
        public CheckoutPaymentMappingProfile()
        {
            CreateMap<PaymentDto, CheckoutPayment>();
        }
    }
}