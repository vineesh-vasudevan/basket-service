using AutoMapper;
using Basket.Contracts.Models.Basket.Output;
using Basket.Contracts.Models.BasketItem.Output;
using Basket.Domain.Entities;

namespace Basket.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketItem, BasketItemDto>();

            CreateMap<Domain.Entities.Basket, BasketDto>()
               .ForMember(destination => destination.Items, opt => opt.MapFrom(src => src.BasketItems))
               .ForMember(destination => destination.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));
        }
    }
}
