using OrderIntegration.Domain.Common;
using OrderIntegration.Domain.Enums;
using OrderIntegration.Domain.Exceptions;

namespace OrderIntegration.Domain.Entities;

public class Order : BaseEntity
{
    private readonly List<OrderItem> _items = new();

    private Order()
    {
    }

    public Order(
        string externalOrderId,
        string customerName,
        string customerEmail,
        decimal totalAmount,
        string currency)
    {
        if (string.IsNullOrWhiteSpace(externalOrderId))
            throw new DomainException("External order id is required.");

        if (string.IsNullOrWhiteSpace(customerName))
            throw new DomainException("Customer name is required.");

        if (string.IsNullOrWhiteSpace(customerEmail))
            throw new DomainException("Customer email is required.");

        if (totalAmount <= 0)
            throw new DomainException("Total amount must be greater than zero.");

        if (string.IsNullOrWhiteSpace(currency))
            throw new DomainException("Currency is required.");

        ExternalOrderId = externalOrderId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        TotalAmount = totalAmount;
        Currency = currency.ToUpperInvariant();

        Status = OrderStatus.Pending;
        IntegrationStatus = IntegrationStatus.Pending;
    }

    public string ExternalOrderId { get; private set; } = string.Empty;

    public string CustomerName { get; private set; } = string.Empty;

    public string CustomerEmail { get; private set; } = string.Empty;

    public decimal TotalAmount { get; private set; }

    public string Currency { get; private set; } = string.Empty;

    public OrderStatus Status { get; private set; }

    public IntegrationStatus IntegrationStatus { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public void AddItem(string productName, int quantity, decimal unitPrice)
    {
        var item = new OrderItem(Id, productName, quantity, unitPrice);
        _items.Add(item);

        SetUpdatedAt();
    }

    public void MarkAsProcessing()
    {
        IntegrationStatus = IntegrationStatus.Processing;
        SetUpdatedAt();
    }

    public void MarkAsSynced()
    {
        IntegrationStatus = IntegrationStatus.Synced;
        Status = OrderStatus.Confirmed;
        SetUpdatedAt();
    }

    public void MarkAsFailed()
    {
        IntegrationStatus = IntegrationStatus.Failed;
        Status = OrderStatus.Failed;
        SetUpdatedAt();
    }

    public void MarkAsRetrying()
    {
        IntegrationStatus = IntegrationStatus.Retrying;
        SetUpdatedAt();
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Confirmed)
            throw new DomainException("Confirmed orders cannot be cancelled.");

        Status = OrderStatus.Cancelled;
        SetUpdatedAt();
    }
}

