using AutoMapper;
using Basket.Application.MappingProfile;
using Basket.CheckOutEvent;
using Basket.Mocks.Dtos;
using FluentAssertions;

namespace Basket.Application.Tests.MappingProfile
{
    [TestFixture]
    public class CheckoutAddressMappingProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CheckoutAddressMappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Test]
        public void Should_Map_AddressDto_To_CheckoutAddress_Correctly()
        {
            // Arrange
            var dto = AddressDtoBuilder.Default();

            // Act
            var result = _mapper.Map<CheckoutAddress>(dto);

            // Assert
            result.FirstName.Should().Be(dto.FirstName);
            result.LastName.Should().Be(dto.LastName);
            result.Street.Should().Be(dto.Street);
            result.City.Should().Be(dto.City);
            result.State.Should().Be(dto.State);
            result.PostalCode.Should().Be(dto.PostalCode);
            result.Country.Should().Be(dto.Country);
        }

        [Test]
        public void AutoMapper_Configuration_IsValid()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddProfile<CheckoutAddressMappingProfile>());

            // Act & Assert
            config.AssertConfigurationIsValid();
        }
    }
}