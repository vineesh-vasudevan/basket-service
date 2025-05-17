using AutoMapper;
using Basket.Application.MappingProfile;
using Basket.Contracts.Dtos.Common;
using Basket.Contracts.Events;
using Basket.Domain.Entities;
using Basket.Domain.Enum;
using FluentAssertions;

namespace Basket.Application.Tests.MappingProfile
{
    [TestFixture]
    public class BasketItemMappingProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BasketItemMappingProfile>();
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
        public void Should_Map_BasketItem_To_BasketItemEvent_Correctly()
        {
            // Arrange
            var basketItemId = Guid.NewGuid();
            var basketId = Guid.NewGuid();
            var domainItem = BasketItem.Create(basketId, basketItemId, "PROD-123", "Red", 99.99m, 2, BasketItemStatus.Active);

            // Act
            var eventItem = _mapper.Map<BasketItemEvent>(domainItem);

            // Assert
            eventItem.Id.Should().Be(basketItemId);
            eventItem.BasketId.Should().Be(basketId);
            eventItem.ProductCode.Should().Be("PROD-123");
            eventItem.Color.Should().Be("Red");
            eventItem.Price.Should().Be(99.99m);
            eventItem.Quantity.Should().Be(2);
            eventItem.Status.Should().Be(BasketItemStatusDto.Active);
        }
    }
}