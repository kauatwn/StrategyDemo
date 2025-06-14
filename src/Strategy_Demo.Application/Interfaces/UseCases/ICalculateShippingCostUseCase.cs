using Strategy_Demo.Domain.Entities;

namespace Strategy_Demo.Application.Interfaces.UseCases;

public interface ICalculateShippingCostUseCase
{
    double Execute(Order order);
}