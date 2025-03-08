using Domain.Enums;
using Domain.Interfaces.Strategies;
using Infrastructure.Factories;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Infrastructure.UnitTests.Factories;

public class ShippingStrategyFactoryTests
{
    private Mock<IKeyedServiceProvider> MockServiceProvider { get; } = new();
    private ShippingStrategyFactory Factory { get; }

    public ShippingStrategyFactoryTests()
    {
        Factory = new ShippingStrategyFactory(MockServiceProvider.Object);
    }

    [Fact]
    public void ShouldCreateShippingStrategyWhenValidShippingMethodIsProvided()
    {
        // Arrange
        const ShippingMethod method = ShippingMethod.Standard;

        MockServiceProvider.Setup(s => s.GetKeyedService(typeof(IShippingStrategy), method))
            .Returns(Mock.Of<IShippingStrategy>());

        // Act
        IShippingStrategy? result = Factory.Create(method);

        // Assert
        Assert.NotNull(result);

        MockServiceProvider.Verify(s => s.GetKeyedService(typeof(IShippingStrategy), method), Times.Once);
    }

    [Fact]
    public void ShouldReturnNullWhenInvalidShippingMethodIsProvided()
    {
        // Arrange
        const ShippingMethod method = (ShippingMethod)int.MaxValue;

        MockServiceProvider.Setup(s => s.GetKeyedService(typeof(IShippingStrategy), method))
            .Returns((IShippingStrategy?)null);

        // Act
        IShippingStrategy? result = Factory.Create(method);

        // Assert
        Assert.Null(result);

        MockServiceProvider.Verify(s => s.GetKeyedService(typeof(IShippingStrategy), method), Times.Once);
    }
}