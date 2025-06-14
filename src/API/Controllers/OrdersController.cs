using API.DTOs.Responses;
using Application.Interfaces.UseCases;
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
        var method = order.ShippingMethod.ToString();
        ShippingCostResponse response = new($"The {method.ToLower()} shipping cost is {cost:C}", cost, method);

        return Ok(response);
    }
}