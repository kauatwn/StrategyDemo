using Microsoft.AspNetCore.Mvc;
using StrategyDemo.API.DTOs.Responses;
using StrategyDemo.Application.Interfaces.UseCases;
using StrategyDemo.Domain.Entities;

namespace StrategyDemo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class OrdersController : ControllerBase
{
    [HttpPost("CalculateShippingCost")]
    [ProducesResponseType<ShippingCostResponse>(StatusCodes.Status200OK)]
    public ActionResult<ShippingCostResponse> CalculateShippingCost(ICalculateShippingCostUseCase useCase, Order order)
    {
        double cost = useCase.Execute(order);
        var method = order.ShippingMethod.ToString();
        ShippingCostResponse response = new($"The {method.ToLower()} shipping cost is {cost:C}", cost, method);

        return Ok(response);
    }
}