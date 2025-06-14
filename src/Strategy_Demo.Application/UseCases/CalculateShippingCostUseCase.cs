using Strategy_Demo.Domain.Entities;
using Strategy_Demo.Application.Interfaces.Contexts;
using Strategy_Demo.Application.Interfaces.UseCases;

namespace Strategy_Demo.Application.UseCases;

public class CalculateShippingCostUseCase(IShippingContext context) : ICalculateShippingCostUseCase
{
    public double Execute(Order order)
    {
        context.SetStrategy(order.ShippingMethod);
        return context.CalculateShippingCost(order);
    }
}