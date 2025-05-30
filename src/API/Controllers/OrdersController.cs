using API.DTOs.Responses;
using Application.Abstractions.UseCases;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    [HttpPost("CalculateShippingCost")]
    [ProducesResponseType<ShippingCostResponse>(StatusCodes.Status200OK)]
    public ActionResult<ShippingCostResponse> CalculateShippingCost(ICalculateShippingCostUseCase useCase, Order order)
    {
        double cost = useCase.Execute(order);
        ShippingCostResponse response =
            new($"The {order.ShippingMethod.ToString().ToLower()} shipping cost is {cost:C}", cost,
                order.ShippingMethod.ToString());

        return Ok(response);
    }
}