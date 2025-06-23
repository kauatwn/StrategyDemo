using StrategyDemo.Domain.Entities;

namespace StrategyDemo.Application.Interfaces.UseCases;

public interface ICalculateShippingCostUseCase
{
    double Execute(Order order);
}