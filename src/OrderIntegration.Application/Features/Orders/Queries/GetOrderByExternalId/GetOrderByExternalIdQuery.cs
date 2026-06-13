using MediatR;
using OrderIntegration.Application.Common.Models;
using OrderIntegration.Application.Features.Orders.Dtos;

namespace OrderIntegration.Application.Features.Orders.Queries.GetOrderByExternalId
{
    public record GetOrderByExternalIdQuery(string ExternalOrderId) : IRequest<Result<OrderResponse>>;
}
