using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderIntegration.Application.Common.Interfaces;
using OrderIntegration.Application.Common.Models;
using OrderIntegration.Application.Features.Orders.Dtos;
using OrderIntegration.Domain.Entities;

namespace OrderIntegration.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler
    : IRequestHandler<CreateOrderCommand, Result<OrderResponse>>
{
    private readonly IAppDbContext _context;

    public CreateOrderCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<OrderResponse>> Handle(
        CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _context.Orders.AnyAsync(x => x.ExternalOrderId == request.ExternalOrderId, cancellationToken);

        if (exists)
            return Result<OrderResponse>.Failure($"An order with external id '{request.ExternalOrderId}' already exists.");

        var order = new Order(
            request.ExternalOrderId,
            request.CustomerName,
            request.CustomerEmail,
            request.TotalAmount,
            request.Currency);

        foreach (var item in request.Items)
            order.AddItem(item.ProductName, item.Quantity, item.UnitPrice);

        _context.Orders.Add(order);

        await _context.SaveChangesAsync(cancellationToken);

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
            Items = order.Items.Select(x => new OrderItemResponse
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.TotalPrice
            }).ToList()
        };

        return Result<OrderResponse>.Success(response);
    }
}