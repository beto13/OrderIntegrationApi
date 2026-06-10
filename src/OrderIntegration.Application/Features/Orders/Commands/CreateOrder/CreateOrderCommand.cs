using MediatR;
using OrderIntegration.Application.Common.Models;
using OrderIntegration.Application.Features.Orders.Dtos;

namespace OrderIntegration.Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    string ExternalOrderId,
    string CustomerName,
    string CustomerEmail,
    decimal TotalAmount,
    string Currency,
    List<CreateOrderItemCommand> Items
) : IRequest<Result<OrderResponse>>;

public record CreateOrderItemCommand(
    string ProductName,
    int Quantity,
    decimal UnitPrice
);