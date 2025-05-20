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