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