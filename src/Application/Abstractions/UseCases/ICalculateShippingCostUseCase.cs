using Application.DTOs;
using Domain.Entities;

namespace Application.Abstractions.UseCases;

public interface ICalculateShippingCostUseCase
{
    ShippingCostResponse Execute(Order order);
}