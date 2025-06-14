using Strategy_Demo.Domain.Entities;

namespace Strategy_Demo.Domain.Interfaces.Strategies;

public interface IShippingStrategy
{
    double Calculate(Order order);
}