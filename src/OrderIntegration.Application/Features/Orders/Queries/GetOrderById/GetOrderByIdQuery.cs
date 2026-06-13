using MediatR;
using OrderIntegration.Application.Common.Models;
using OrderIntegration.Application.Features.Orders.Dtos;

namespace OrderIntegration.Application.Features.Orders.Queries.GetOrderById
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<Result<OrderResponse>>;
}
