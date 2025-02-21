using Domain.Entities;

namespace Domain.Interfaces.Strategies;

public interface IShippingStrategy
{
    double Calculate(Order order);
}