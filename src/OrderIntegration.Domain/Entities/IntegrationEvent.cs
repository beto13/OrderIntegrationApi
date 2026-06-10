using OrderIntegration.Domain.Common;
using OrderIntegration.Domain.Enums;
using OrderIntegration.Domain.Exceptions;

namespace OrderIntegration.Domain.Entities;

public class IntegrationEvent : BaseEntity
{
    private IntegrationEvent()
    {
    }

    public IntegrationEvent(
        Guid orderId,
        string eventType,
        string payload)
    {
        if (orderId == Guid.Empty)
            throw new DomainException("Order id is required.");

        if (string.IsNullOrWhiteSpace(eventType))
            throw new DomainException("Event type is required.");

        OrderId = orderId;
        EventType = eventType;
        Payload = payload;
        Status = IntegrationEventStatus.Pending;
    }

    public Guid OrderId { get; private set; }

    public string EventType { get; private set; } = string.Empty;

    public string Payload { get; private set; } = string.Empty;

    public IntegrationEventStatus Status { get; private set; }

    public string? ErrorMessage { get; private set; }

    public DateTime? ProcessedAt { get; private set; }

    public void MarkAsProcessing()
    {
        Status = IntegrationEventStatus.Processing;
        SetUpdatedAt();
    }

    public void MarkAsCompleted()
    {
        Status = IntegrationEventStatus.Completed;
        ProcessedAt = DateTime.UtcNow;
        SetUpdatedAt();
    }

    public void MarkAsFailed(string errorMessage)
    {
        Status = IntegrationEventStatus.Failed;
        ErrorMessage = errorMessage;
        SetUpdatedAt();
    }
}