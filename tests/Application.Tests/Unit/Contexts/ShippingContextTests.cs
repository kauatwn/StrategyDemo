using Application.Contexts;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Factories;
using Domain.Interfaces.Strategies;
using Moq;

namespace Application.Tests.Unit.Contexts;

public class ShippingContextTests
{
    private readonly Mock<IShippingStrategyFactory> _mockFactory = new();
    private readonly Mock<IShippingStrategy> _mockStrategy = new();

    private readonly ShippingContext _context;

    public ShippingContextTests() => _context = new ShippingContext(_mockFactory.Object);

    [Fact]
    public void ShouldCalculateShippingCostWhenStrategyIsSet()
    {
        // Arrange
        Order order = new(weight: 10.0, distance: 100.0, shippingMethod: ShippingMethod.Standard);
        const double expectedCost = 10.0;

        _mockFactory.Setup(f => f.Create(It.IsAny<ShippingMethod>())).Returns(_mockStrategy.Object);
        _mockStrategy.Setup(s => s.Calculate(order)).Returns(expectedCost);

        _context.SetStrategy(order.ShippingMethod);

        // Act
        double result = _context.CalculateShippingCost(order);

        // Assert
        Assert.Equal(expectedCost, result);

        _mockFactory.Verify(f => f.Create(order.ShippingMethod), Times.Once,
            "A fábrica deve ser chamada para criar a estratégia de envio");

        _mockStrategy.Verify(s => s.Calculate(order), Times.Once,
            "O contexto deve delegar o cálculo de custo para a estratégia configurada");
    }

    [Fact]
    public void ShouldThrowInvalidOperationExceptionWhenStrategyIsNotSetBeforeCalculation()
    {
        // Arrange
        Order order = new(weight: 10.0, distance: 100.0, shippingMethod: ShippingMethod.Standard);

        // Act
        Action act = () => _context.CalculateShippingCost(order);

        // Assert
        Assert.Throws<InvalidOperationException>(act);

        _mockFactory.Verify(f => f.Create(It.IsAny<ShippingMethod>()), Times.Never,
            "A fábrica não deve ser chamada quando a estratégia não foi definida");

        _mockStrategy.Verify(s => s.Calculate(It.IsAny<Order>()), Times.Never,
            "A estratégia não deve ser usada quando não foi definida");
    }
}