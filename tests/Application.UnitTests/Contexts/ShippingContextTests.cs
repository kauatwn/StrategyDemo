using Application.Contexts;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;
using Moq;

namespace Application.UnitTests.Contexts;

public class ShippingContextTests
{
    private Mock<IShippingStrategyFactory> MockFactory { get; } = new();
    private Mock<IShippingStrategy> MockStrategy { get; } = new();
    private ShippingContext Context { get; }

    public ShippingContextTests()
    {
        Context = new ShippingContext(MockFactory.Object);
    }

    [Fact]
    public void ShouldSetStrategyWhenFactoryReturnsStrategy()
    {
        // Arrange
        const ShippingMethod method = ShippingMethod.Standard;

        MockFactory.Setup(f => f.Create(It.IsAny<ShippingMethod>())).Returns(MockStrategy.Object);

        // Act
        Context.SetStrategy(method);

        // Assert
        Assert.NotNull(Context.Strategy);
        Assert.Same(MockStrategy.Object, Context.Strategy);

        MockFactory.Verify(f => f.Create(It.IsAny<ShippingMethod>()), Times.Once);
    }

    [Fact]
    public void ShouldKeepStrategyNullWhenFactoryReturnsNull()
    {
        // Arrange
        const ShippingMethod method = ShippingMethod.Standard;

        MockFactory.Setup(f => f.Create(It.IsAny<ShippingMethod>())).Returns((IShippingStrategy?)null);

        // Act
        Context.SetStrategy(method);

        // Assert
        Assert.Null(Context.Strategy);

        MockFactory.Verify(f => f.Create(It.IsAny<ShippingMethod>()), Times.Once);
    }

    [Fact]
    public void ShouldCalculateShippingCostWhenStrategyIsSet()
    {
        // Arrange
        var order = new Order(10, 100, ShippingMethod.Standard);

        MockFactory.Setup(f => f.Create(It.IsAny<ShippingMethod>())).Returns(MockStrategy.Object);
        MockStrategy.Setup(s => s.Calculate(It.IsAny<Order>()));

        Context.SetStrategy(order.ShippingMethod);

        // Act
        Context.CalculateShippingCost(order);

        // Assert
        MockFactory.Verify(f => f.Create(It.IsAny<ShippingMethod>()), Times.Once);
        MockStrategy.Verify(s => s.Calculate(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public void ShouldThrowInvalidOperationExceptionWhenStrategyIsNotSet()
    {
        // Arrange
        var order = new Order(10, 100, ShippingMethod.Standard);
        var message = $"No shipping strategy found for method '{order.ShippingMethod}'.";

        // Act
        Action act = () => Context.CalculateShippingCost(order);

        // Assert
        var exception = Assert.Throws<InvalidOperationException>(act);
        Assert.Equal(message, exception.Message);

        MockFactory.Verify(f => f.Create(It.IsAny<ShippingMethod>()), Times.Never);
        MockStrategy.Verify(s => s.Calculate(It.IsAny<Order>()), Times.Never);
    }
}