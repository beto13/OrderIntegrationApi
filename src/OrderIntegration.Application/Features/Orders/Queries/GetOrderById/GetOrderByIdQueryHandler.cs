using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderIntegration.Application.Common.Interfaces;
using OrderIntegration.Application.Common.Models;
using OrderIntegration.Application.Features.Orders.Dtos;

namespace OrderIntegration.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderResponse>>
    {
        private readonly IAppDbContext appDbContext;

        public GetOrderByIdQueryHandler(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await appDbContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (order == null) 
                return Result<OrderResponse>.Failure($"Order with id '{request.Id}' was not found.");

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
