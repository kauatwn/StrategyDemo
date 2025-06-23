using StrategyDemo.Domain.Entities;
using StrategyDemo.Domain.Enums;

namespace StrategyDemo.Application.Interfaces.Contexts;

public interface IShippingContext
{
    void SetStrategy(ShippingMethod method);
    double CalculateShippingCost(Order order);
}