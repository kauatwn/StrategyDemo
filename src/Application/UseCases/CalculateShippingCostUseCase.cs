using Application.Abstractions.Contexts;
using Application.Abstractions.UseCases;
using Application.DTOs;
using Domain.Entities;

namespace Application.UseCases;

public class CalculateShippingCostUseCase(IShippingContext context) : ICalculateShippingCostUseCase
{
    public ShippingCostResponse Execute(Order order)
    {
        context.SetStrategy(order.ShippingMethod);

        double cost = context.CalculateShippingCost(order);
        var message = $"The {order.ShippingMethod.ToString().ToLower()} shipping cost is {cost:C}";

        return new ShippingCostResponse(message, cost, order.ShippingMethod.ToString());
    }
}