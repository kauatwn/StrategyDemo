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
    }

    [Fact]
    public void ShouldPropagateInvalidOperationExceptionWhenResolverThrowsDuringCreate()
    {
        // Arrange
        const ShippingMethod invalidMethod = (ShippingMethod)int.MaxValue;
        InvalidOperationException expected = new();

        _mockResolver.Setup(r => r(invalidMethod)).Throws(expected);

        // Act
        Action act = () => _factory.Create(invalidMethod);

        // Assert
        var actual = Assert.Throws<InvalidOperationException>(act);
        Assert.Same(expected, actual);

        _mockResolver.Verify(r => r(invalidMethod), Times.Once,
            $"O resolver deve ser chamado mesmo para métodos inválidos ({invalidMethod}).");
    }
}