using AutoMapper;
using Basket.Contracts.Dtos.Basket.Output;
using Basket.Contracts.Dtos.BasketItem.Output;
using Basket.Contracts.Dtos.Common;
using Basket.Domain.Entities;

namespace Basket.Application.MappingProfile
{
    public class BasketDtoMappingProfile : Profile
    {
        public BasketDtoMappingProfile()
        {
            CreateMap<BasketItem, BasketItemDto>()
                  .ForMember(x => x.Status,
                    o => o.MapFrom(s => Enum.Parse<BasketItemStatusDto>(s.Status.Name)));

            CreateMap<Domain.Entities.Basket, BasketDto>()
                .ForMember(x => x.Status,
                    o => o.MapFrom(s => Enum.Parse<BasketStatusDto>(s.Status.Name)))
               .ForMember(destination => destination.Items, opt => opt.MapFrom(src => src.BasketItems))
               .ForMember(destination => destination.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));
        }
    }
}