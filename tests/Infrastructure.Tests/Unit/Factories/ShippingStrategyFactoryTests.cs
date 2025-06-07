using Domain.Enums;
using Domain.Interfaces.Strategies;
using Infrastructure.Factories;
using Moq;

namespace Infrastructure.Tests.Unit.Factories;

public class ShippingStrategyFactoryTests
{
    private readonly Mock<Func<ShippingMethod, IShippingStrategy>> _mockResolver = new();
    private readonly ShippingStrategyFactory _factory;

    public ShippingStrategyFactoryTests() => _factory = new ShippingStrategyFactory(_mockResolver.Object);

    [Theory]
    [InlineData(ShippingMethod.Standard)]
    [InlineData(ShippingMethod.Express)]
    public void ShouldCreateShippingStrategyWhenValidShippingMethodIsProvided(ShippingMethod method)
    {
        // Arrange
        var expected = Mock.Of<IShippingStrategy>();

        _mockResolver.Setup(r => r(method)).Returns(expected);

        // Act
        IShippingStrategy result = _factory.Create(method);

        // Assert
        Assert.NotNull(result);
        Assert.Same(expected, result);

        _mockResolver.Verify(r => r(method), Times.Once, $"O resolver não foi chamado corretamente para {method}.");
    }

    [Fact]
    public void ShouldThrowInvalidOperationExceptionWhenInvalidShippingMethodIsProvided()
    {
        // Arrange
        const ShippingMethod invalidMethod = (ShippingMethod)int.MaxValue;

        _mockResolver.Setup(r => r(invalidMethod)).Throws<InvalidOperationException>();

        // Act
        Action act = () => _factory.Create(invalidMethod);

        // Assert
        Assert.Throws<InvalidOperationException>(act);

        _mockResolver.Verify(r => r(invalidMethod), Times.Once,
            $"O resolver deve ser chamado mesmo para métodos inválidos ({invalidMethod}).");
    }
}