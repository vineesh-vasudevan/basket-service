using AutoMapper;
using Basket.Contracts.Dtos.Common;
using Basket.Contracts.Events;
using Basket.Domain.Entities;

namespace Basket.Application.MappingProfile
{
    public class BasketItemMappingProfile : Profile
    {
        public BasketItemMappingProfile()
        {
            CreateMap<BasketItem, BasketItemEvent>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.ProductCode, opt => opt.MapFrom(src => src.ProductCode))
                .ForMember(d => d.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(d => d.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(d => d.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(d => d.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(d => d.BasketId, opt => opt.MapFrom(src => src.BasketId))
                .ForMember(d => d.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(d => d.Status, opt => opt.MapFrom(src => (BasketItemStatusDto)src.Status.Value));
        }
    }
}