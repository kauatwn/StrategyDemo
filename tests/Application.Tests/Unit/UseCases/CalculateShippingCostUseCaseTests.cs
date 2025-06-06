using Application.Abstractions.Contexts;
using Application.UseCases;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace Application.Tests.Unit.UseCases;

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
            "O Use Case deve configurar a estratégia correta."
        );

        _mockContext.Verify(c => c.CalculateShippingCost(order), Times.Once,
            "O Use Case deve repassar a Order correta para o cálculo."
        );
    }
}