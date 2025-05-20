using AutoMapper;
using Basket.CheckOutEvent;
using Basket.Contracts.Dtos.BasketCheckout;

namespace Basket.Application.MappingProfile
{
    public class CheckoutAddressMappingProfile : Profile
    {
        public CheckoutAddressMappingProfile()
        {
            CreateMap<AddressDto, CheckoutAddress>();
        }
    }
}