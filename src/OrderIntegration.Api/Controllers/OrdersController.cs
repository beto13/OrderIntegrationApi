using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderIntegration.Application.Features.Orders.Commands.CreateOrder;
using OrderIntegration.Application.Features.Orders.Queries.GetOrderByExternalId;
using OrderIntegration.Application.Features.Orders.Queries.GetOrderById;

namespace OrderIntegration.Api.Controllers;

[ApiController]
[Route("api/v1/orders")]
public class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Conflict(new
            {
                code = "ORDER_ALREADY_EXISTS",
                message = result.Error,
                traceId = HttpContext.TraceIdentifier
            });
        }

        return Accepted(result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetOrderByIdQuery(id), cancellationToken);


        if (result.IsFailure)
        {
            return NotFound(new
            {
                code = "ORDER_NOT_FOUND",
                message = result.Error,
                traceId = HttpContext.TraceIdentifier
            });
        }

        return Ok(result.Value);
    }

    [HttpGet("external/{externalOrderId}")]
    public async Task<IActionResult> GetByExternalOrderId(
    string externalOrderId,
    CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetOrderByExternalIdQuery(externalOrderId), cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(new
            {
                code = "ORDER_NOT_FOUND",
                message = result.Error,
                traceId = HttpContext.TraceIdentifier
            });
        }

        return Ok(result.Value);
    }
}