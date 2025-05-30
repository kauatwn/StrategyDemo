using Domain.Entities;

namespace Application.Abstractions.UseCases;

public interface ICalculateShippingCostUseCase
{
    double Execute(Order order);
}