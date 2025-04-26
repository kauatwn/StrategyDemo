using Application.Abstractions.Contexts;
using Application.Abstractions.UseCases;
using Application.DTOs;
using Domain.Entities;

namespace Application.UseCases;

public class CalculateShippingCostUseCase(IShippingContext context) : ICalculateShippingCostUseCase
{
    private readonly IShippingContext _context = context;

    public ShippingCostResponse Execute(Order order)
    {
        _context.SetStrategy(order.ShippingMethod);

        double cost = _context.CalculateShippingCost(order);
        var message = $"The {order.ShippingMethod.ToString().ToLower()} shipping cost is {cost:C}";

        return new ShippingCostResponse(message, cost, order.ShippingMethod.ToString());
    }
}