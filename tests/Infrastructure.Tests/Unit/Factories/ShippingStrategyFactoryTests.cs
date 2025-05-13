using Domain.Enums;
using Domain.Interfaces.Strategies;
using Infrastructure.Factories;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Infrastructure.Tests.Unit.Factories;

public class ShippingStrategyFactoryTests
{
    private readonly Mock<IKeyedServiceProvider> _mockServiceProvider = new();
    private readonly ShippingStrategyFactory _factory;

    public ShippingStrategyFactoryTests() => _factory = new ShippingStrategyFactory(_mockServiceProvider.Object);

    [Theory]
    [InlineData(ShippingMethod.Standard)]
    [InlineData(ShippingMethod.Express)]
    public void ShouldCreateShippingStrategyWhenValidShippingMethodIsProvided(ShippingMethod method)
    {
        // Arrange
        var expected = Mock.Of<IShippingStrategy>();

        _mockServiceProvider.Setup(s => s.GetKeyedService(typeof(IShippingStrategy), method))
            .Returns(expected);

        // Act
        IShippingStrategy? result = _factory.Create(method);

        // Assert
        Assert.NotNull(result);
        Assert.Same(expected, result);

        _mockServiceProvider.Verify(s => s.GetKeyedService(typeof(IShippingStrategy), method), Times.Once);
    }

    [Fact]
    public void ShouldReturnNullWhenInvalidShippingMethodIsProvided()
    {
        // Arrange
        const ShippingMethod method = (ShippingMethod)int.MaxValue;

        _mockServiceProvider.Setup(s => s.GetKeyedService(typeof(IShippingStrategy), method))
            .Returns((IShippingStrategy?)null);

        // Act
        IShippingStrategy? result = _factory.Create(method);

        // Assert
        Assert.Null(result);

        _mockServiceProvider.Verify(s => s.GetKeyedService(typeof(IShippingStrategy), method), Times.Once);
    }
}