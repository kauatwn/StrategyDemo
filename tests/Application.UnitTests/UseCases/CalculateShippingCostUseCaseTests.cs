using Application.Abstractions.Contexts;
using Application.UseCases;
using Domain.Entities;
using Domain.Enums;
using Moq;

namespace Application.UnitTests.UseCases;

public class CalculateShippingCostUseCaseTests
{
    private readonly Mock<IShippingContext> _mockContext = new();
    private readonly CalculateShippingCostUseCase _useCase;

    public CalculateShippingCostUseCaseTests()
    {
        _useCase = new CalculateShippingCostUseCase(_mockContext.Object);
    }

    [Fact]
    public void ShouldCalculateShippingCost()
    {
        // Arrange
        var order = new Order(weight: 10, distance: 100, shippingMethod: ShippingMethod.Standard);

        _mockContext.Setup(c => c.SetStrategy(It.IsAny<ShippingMethod>()));
        _mockContext.Setup(c => c.CalculateShippingCost(It.IsAny<Order>()));

        // Act
        _useCase.Execute(order);

        // Assert
        _mockContext.Verify(c => c.SetStrategy(It.IsAny<ShippingMethod>()), Times.Once);
        _mockContext.Verify(c => c.CalculateShippingCost(It.IsAny<Order>()), Times.Once);
    }
}