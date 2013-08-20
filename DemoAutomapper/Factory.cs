using System;
using AutoMapper;
using Xunit;

namespace DemoAutomapper
{
    public class AutomapperTests
    {
        private static Order CreateComplexObject()
        {
            var customer = new Customer { Name = "George Costanza" };
            var order = new Order { Customer = customer };
            var bosco = new Product 
                            {
                                Name = "Bosco",
                                Price = 4.99m
                            };
            order.AddOrderLineItem(bosco, 15);

            return order;
        }

        [Fact]
        public void should_map_from_complex_object_to_DTO()
        {
            // Arrange
            var order = CreateComplexObject();

            // Act
            Mapper.CreateMap<Order, OrderDto>();
            OrderDto dto = Mapper.Map<Order, OrderDto>(order);

            // Assert
            Assert.Equal("George Costanza", dto.CustomerName);
            Assert.Equal(74.85m, dto.Total);

        }

        [Fact]
        public void should_test_my_mapping()
        {
            // Act
            Mapper.CreateMap<Order, OrderDto>();

            // Assert
            Mapper.AssertConfigurationIsValid();
        }
    }
}
