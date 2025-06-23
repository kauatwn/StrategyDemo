using StrategyDemo.Domain.Enums;
using StrategyDemo.Domain.Interfaces.Strategies;

namespace StrategyDemo.Domain.Interfaces.Factories;

public interface IShippingStrategyFactory
{
    IShippingStrategy Create(ShippingMethod method);
}