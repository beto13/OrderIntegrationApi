using OrderIntegration.Domain.Common;
using OrderIntegration.Domain.Exceptions;

namespace OrderIntegration.Domain.Entities;

public class OrderItem : BaseEntity
{
    private OrderItem()
    {
    }

    public OrderItem(
        Guid orderId,
        string productName,
        int quantity,
        decimal unitPrice)
    {
        if (orderId == Guid.Empty)
            throw new DomainException("Order id is required.");

        if (string.IsNullOrWhiteSpace(productName))
            throw new DomainException("Product name is required.");

        if (quantity <= 0)
            throw new DomainException("Quantity must be greater than zero.");

        if (unitPrice <= 0)
            throw new DomainException("Unit price must be greater than zero.");

        OrderId = orderId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public Guid OrderId { get; private set; }

    public string ProductName { get; private set; } = string.Empty;

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal TotalPrice => Quantity * UnitPrice;
}