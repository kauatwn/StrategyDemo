using Application.DTOs;
using Domain.Entities;

namespace Application.Abstractions.UseCases;

public interface ICalculateShippingCostUseCase
{
    ShippingCostResult Execute(Order order);
}