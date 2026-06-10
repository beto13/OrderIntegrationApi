using OrderIntegration.Domain.Common;
using OrderIntegration.Domain.Enums;
using OrderIntegration.Domain.Exceptions;

namespace OrderIntegration.Domain.Entities;

public class WebhookEvent : BaseEntity
{
    private WebhookEvent()
    {
    }

    public WebhookEvent(
        string externalEventId,
        string provider,
        string eventType,
        string payload)
    {
        if (string.IsNullOrWhiteSpace(externalEventId))
            throw new DomainException("External event id is required.");

        if (string.IsNullOrWhiteSpace(provider))
            throw new DomainException("Provider is required.");

        if (string.IsNullOrWhiteSpace(eventType))
            throw new DomainException("Event type is required.");

        ExternalEventId = externalEventId;
        Provider = provider;
        EventType = eventType;
        Payload = payload;
        Status = WebhookEventStatus.Received;
    }

    public string ExternalEventId { get; private set; } = string.Empty;

    public string Provider { get; private set; } = string.Empty;

    public string EventType { get; private set; } = string.Empty;

    public string Payload { get; private set; } = string.Empty;

    public WebhookEventStatus Status { get; private set; }

    public string? ErrorMessage { get; private set; }

    public DateTime? ProcessedAt { get; private set; }

    public void MarkAsProcessing()
    {
        Status = WebhookEventStatus.Processing;
        SetUpdatedAt();
    }

    public void MarkAsProcessed()
    {
        Status = WebhookEventStatus.Processed;
        ProcessedAt = DateTime.UtcNow;
        SetUpdatedAt();
    }

    public void MarkAsFailed(string errorMessage)
    {
        Status = WebhookEventStatus.Failed;
        ErrorMessage = errorMessage;
        SetUpdatedAt();
    }

    public void MarkAsDuplicated()
    {
        Status = WebhookEventStatus.Duplicated;
        SetUpdatedAt();
    }
}