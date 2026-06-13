using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderIntegration.Application.Common.Interfaces;
using OrderIntegration.Application.Common.Models;
using OrderIntegration.Application.Features.Orders.Dtos;

namespace OrderIntegration.Application.Features.Orders.Queries.GetOrderByExternalId
{
    public class GetOrderByExternalIdQueryHandler : IRequestHandler<GetOrderByExternalIdQuery, Result<OrderResponse>>
    {
        private readonly IAppDbContext appDbContext;

        public GetOrderByExternalIdQueryHandler(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Result<OrderResponse>> Handle(GetOrderByExternalIdQuery request, CancellationToken cancellationToken)
        {
            var order = await appDbContext.Orders
                .Include(o=>o.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(o=> o.ExternalOrderId == request.ExternalOrderId, cancellationToken);

            if (order is null)
            {
                return Result<OrderResponse>.Failure(
                    $"Order with external id '{request.ExternalOrderId}' was not found.");
            }

            var response = new OrderResponse
            {
                Id = order.Id,
                ExternalOrderId = order.ExternalOrderId,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                TotalAmount = order.TotalAmount,
                Currency = order.Currency,
                Status = order.Status.ToString(),
                IntegrationStatus = order.IntegrationStatus.ToString(),
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(item => new OrderItemResponse
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                }).ToList()
            };

            return Result<OrderResponse>.Success(response);
        }
    }
}
