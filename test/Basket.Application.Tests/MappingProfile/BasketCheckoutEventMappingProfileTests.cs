using AutoMapper;
using Basket.Application.MappingProfile;
using Basket.CheckOutEvent;
using Basket.Mocks.Domain;
using Basket.Mocks.Dtos;
using FluentAssertions;

namespace Basket.Application.Tests.MappingProfile
{
    [TestFixture]
    public class BasketCheckoutEventMappingProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BasketItemEventMappingProfile>();
                cfg.AddProfile<BasketCheckoutEventMappingProfile>();
                cfg.AddProfile<CheckoutPaymentMappingProfile>();
                cfg.AddProfile<CheckoutAddressMappingProfile>();
            });

            _mapper = config.CreateMapper();
        }

        [Test]
        public void Should_have_valid_configuration()
        {
            //Assert
            _mapper
                .ConfigurationProvider
                .AssertConfigurationIsValid();
        }

        [Test]
        public void Should_Map_Basket_And_BasketCheckoutDto_To_BasketCheckoutEvent()
        {
            // Arrange
            var basket = new BasketBuilder()
                .WithCurrency("CHF")
                .AddBasketItem(b => b.WithProductCode("P1").WithPrice(20).WithQuantity(2))
                .AddBasketItem(b => b.WithProductCode("P2").WithPrice(15).WithQuantity(1))
                .Build();

            var basketCheckoutDto = new BasketCheckoutDtoBuilder()
                .WithOrderName("Test Order")
                .WithPayment(new PaymentDtoBuilder().WithAmount(50).WithCurrency("CHF").Build())
                .Build();

            var result = _mapper.Map<BasketCheckoutEvent>((basket, basketCheckoutDto));

            result.Should().NotBeNull();
            result.BasketId.Should().Be(basket.Id);
            result.CustomerId.Should().Be(basket.CustomerId);
            result.OrderName.Should().Be(basketCheckoutDto.OrderName);
            result.Status.Should().Be((BasketCheckoutStatus)basket.Status.Value);
            result.ShippingAddress.Should().BeEquivalentTo(basketCheckoutDto.ShippingAddress);
            result.BillingAddress.Should().BeEquivalentTo(basketCheckoutDto.BillingAddress);
            result.Payment.Should().BeEquivalentTo(basketCheckoutDto.Payment);

            result.Items.Should().HaveCount(2);
            var item = result.Items.First();
            item.ProductCode.Should().Be("P1");
            item.Quantity.Should().Be(2);
            item.Price.Should().Be(20);
        }
    }
}