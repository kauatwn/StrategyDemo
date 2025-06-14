using Domain.Entities;

namespace Application.Interfaces.UseCases;

public interface ICalculateShippingCostUseCase
{
    double Execute(Order order);
}