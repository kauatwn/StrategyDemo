using StrategyDemo.Domain.Entities;

namespace StrategyDemo.Domain.Interfaces.Strategies;

public interface IShippingStrategy
{
    double Calculate(Order order);
}