using AutoMapper;
using Basket.Contracts.Dtos.BasketCheckout;
using Basket.Contracts.Dtos.Common;
using Basket.Contracts.Events;

namespace Basket.Application.MappingProfile
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<(Domain.Entities.Basket basket, BasketCheckoutDto basketCheckoutDto), BasketCheckoutEvent>()
               .ForMember(d => d.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
               .ForMember(d => d.BasketId, opt => opt.MapFrom(src => src.basket.Id))
               .ForMember(d => d.CustomerId, opt => opt.MapFrom(src => src.basket.CustomerId))
               .ForMember(d => d.OrderName, opt => opt.MapFrom(src => src.basketCheckoutDto.OrderName))
               .ForMember(d => d.Status, opt => opt.MapFrom(src => (BasketStatusDto)src.basket.Status.Value))
               .ForMember(d => d.ShippingAddress, opt => opt.MapFrom(src => src.basketCheckoutDto.ShippingAddress))
               .ForMember(d => d.BillingAddress, opt => opt.MapFrom(src => src.basketCheckoutDto.BillingAddress))
               .ForMember(d => d.Payment, opt => opt.MapFrom(src => src.basketCheckoutDto.Payment))
               .ForMember(d => d.CheckedOutAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
               .ForMember(d => d.Items, opt => opt.MapFrom(src => src.basket.BasketItems));
        }
    }
}