using Application.Contexts;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;
using Moq;

namespace Application.UnitTests.Contexts;

public class ShippingContextTests
{
    private readonly Mock<IShippingStrategyFactory> _mockFactory = new();
    private readonly Mock<IShippingStrategy> _mockStrategy = new();

    private readonly ShippingContext _context;

    public ShippingContextTests()
    {
        _context = new ShippingContext(_mockFactory.Object);
    }

    [Fact]
    public void ShouldCalculateShippingCostWhenStrategyIsSet()
    {
        // Arrange
        var order = new Order(weight: 10, distance: 100, shippingMethod: ShippingMethod.Standard);

        _mockFactory.Setup(f => f.Create(It.IsAny<ShippingMethod>())).Returns(_mockStrategy.Object);
        _mockStrategy.Setup(s => s.Calculate(It.IsAny<Order>()));

        _context.SetStrategy(order.ShippingMethod);

        // Act
        _context.CalculateShippingCost(order);

        // Assert
        _mockFactory.Verify(f => f.Create(It.IsAny<ShippingMethod>()), Times.Once);
        _mockStrategy.Verify(s => s.Calculate(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public void ShouldThrowInvalidOperationExceptionWhenStrategyIsNotSetBeforeCalculation()
    {
        // Arrange
        var order = new Order(weight: 10, distance: 100, shippingMethod: ShippingMethod.Standard);

        // Act
        Exception? exception = Record.Exception(() => _context.CalculateShippingCost(order));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<InvalidOperationException>(exception);

        _mockFactory.Verify(f => f.Create(It.IsAny<ShippingMethod>()), Times.Never);
        _mockStrategy.Verify(s => s.Calculate(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public void ShouldThrowInvalidOperationExceptionWhenFactoryReturnsNull()
    {
        // Arrange
        var order = new Order(weight: 10, distance: 100, shippingMethod: ShippingMethod.Standard);

        _mockFactory.Setup(f => f.Create(It.IsAny<ShippingMethod>())).Returns((IShippingStrategy?)null);

        // Act
        Exception? exception = Record.Exception(() => _context.SetStrategy(order.ShippingMethod));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<InvalidOperationException>(exception);

        _mockFactory.Verify(f => f.Create(It.IsAny<ShippingMethod>()), Times.Once);
        _mockStrategy.Verify(s => s.Calculate(It.IsAny<Order>()), Times.Never);
    }
}