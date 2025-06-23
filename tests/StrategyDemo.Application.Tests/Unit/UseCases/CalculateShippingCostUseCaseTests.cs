using Moq;
using StrategyDemo.Application.Interfaces.Contexts;
using StrategyDemo.Application.UseCases;
using StrategyDemo.Domain.Entities;
using StrategyDemo.Domain.Enums;

namespace StrategyDemo.Application.Tests.Unit.UseCases;

public class CalculateShippingCostUseCaseTests
{
    private readonly Mock<IShippingContext> _mockContext = new();
    private readonly CalculateShippingCostUseCase _useCase;

    public CalculateShippingCostUseCaseTests() => _useCase = new CalculateShippingCostUseCase(_mockContext.Object);

    [Fact]
    public void ShouldCalculateShippingCost()
    {
        // Arrange
        Order order = new(weight: 10.0, distance: 100.0, shippingMethod: ShippingMethod.Standard);
        const double expectedCost = 10.0;

        _mockContext.Setup(c => c.CalculateShippingCost(order)).Returns(expectedCost);

        // Act
        double result = _useCase.Execute(order);

        // Assert
        Assert.Equal(expectedCost, result);

        _mockContext.Verify(c => c.SetStrategy(order.ShippingMethod), Times.Once,
            "O Use Case deve configurar a estratégia correta no contexto.");

        _mockContext.Verify(c => c.CalculateShippingCost(order), Times.Once,
            "O Use Case deve repassar a Order correta para o cálculo no contexto.");
    }

    [Fact]
    public void ShouldPropagateInvalidOperationExceptionWhenContextThrowsDuringSetStrategy()
    {
        // Arrange
        Order order = new(weight: 10.0, distance: 100.0, shippingMethod: ShippingMethod.Standard);
        InvalidOperationException expected = new();

        _mockContext.Setup(c => c.SetStrategy(order.ShippingMethod)).Throws(expected);

        // Act
        Action act = () => _useCase.Execute(order);

        // Assert
        var actual = Assert.Throws<InvalidOperationException>(act);
        Assert.Same(expected, actual);

        _mockContext.Verify(c => c.CalculateShippingCost(It.IsAny<Order>()), Times.Never,
            "O método CalculateShippingCost não deveria ser chamado se SetStrategy lançar uma exceção.");
    }

    [Fact]
    public void ShouldPropagateInvalidOperationExceptionWhenContextThrowsDuringCalculateShippingCost()
    {
        // Arrange
        Order order = new(weight: 10.0, distance: 100.0, shippingMethod: ShippingMethod.Standard);
        InvalidOperationException expected = new();

        _mockContext.Setup(c => c.SetStrategy(order.ShippingMethod));
        _mockContext.Setup(c => c.CalculateShippingCost(order)).Throws(expected);

        // Act
        Action act = () => _useCase.Execute(order);

        // Assert
        var actual = Assert.Throws<InvalidOperationException>(act);
        Assert.Same(expected, actual);

        _mockContext.Verify(c => c.SetStrategy(order.ShippingMethod), Times.Once,
            "O método SetStrategy do contexto deve ter sido chamado antes da tentativa de cálculo.");
    }
}