using Application.Abstractions.Contexts;
using Application.Abstractions.UseCases;
using Application.DTOs;
using Domain.Entities;

namespace Application.UseCases;

public class CalculateShippingCostUseCase(IShippingContext context) : ICalculateShippingCostUseCase
{
    public ShippingCostResult Execute(Order order)
    {
        context.SetStrategy(order.ShippingMethod);
        double cost = context.CalculateShippingCost(order);

        return new ShippingCostResult(cost, order.ShippingMethod.ToString());
    }
}