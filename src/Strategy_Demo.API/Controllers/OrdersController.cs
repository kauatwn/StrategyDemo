using Microsoft.AspNetCore.Mvc;
using Strategy_Demo.API.DTOs.Responses;
using Strategy_Demo.Application.Interfaces.UseCases;
using Strategy_Demo.Domain.Entities;

namespace Strategy_Demo.API.Controllers;

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