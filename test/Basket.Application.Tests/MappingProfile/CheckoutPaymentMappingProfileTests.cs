using AutoMapper;
using Basket.Application.MappingProfile;
using Basket.CheckOutEvent;
using Basket.Mocks.Dtos;
using FluentAssertions;

namespace Basket.Application.Tests.MappingProfile
{
    [TestFixture]
    public class CheckoutPaymentMappingProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CheckoutPaymentMappingProfile>());
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
        public void Should_Map_CheckoutPaymentDto_To_CheckoutPayment()
        {
            // Arrange
            var dto = PaymentDtoBuilder.Default();

            // Act
            var result = _mapper.Map<CheckoutPayment>(dto);

            // Assert
            result.Amount.Should().Be(dto.Amount);
            result.Currency.Should().Be(dto.Currency);
            result.PaidAt.Should().Be(dto.PaidAt);
            result.PaymentMethod.Should().Be(dto.PaymentMethod);
            result.IsSuccessful.Should().Be(dto.IsSuccessful);
            result.TransactionId.Should().Be(dto.TransactionId);
        }
    }
}