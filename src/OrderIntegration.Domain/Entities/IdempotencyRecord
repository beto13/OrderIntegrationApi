using OrderIntegration.Domain.Common;
using OrderIntegration.Domain.Exceptions;

namespace OrderIntegration.Domain.Entities;

public class IdempotencyRecord : BaseEntity
{
    private IdempotencyRecord()
    {
    }

    public IdempotencyRecord(
        string key,
        string requestHash,
        string responseBody,
        int statusCode)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new DomainException("Idempotency key is required.");

        if (string.IsNullOrWhiteSpace(requestHash))
            throw new DomainException("Request hash is required.");

        if (string.IsNullOrWhiteSpace(responseBody))
            throw new DomainException("Response body is required.");

        if (statusCode <= 0)
            throw new DomainException("Status code is required.");

        Key = key;
        RequestHash = requestHash;
        ResponseBody = responseBody;
        StatusCode = statusCode;
    }

    public string Key { get; private set; } = string.Empty;

    public string RequestHash { get; private set; } = string.Empty;

    public string ResponseBody { get; private set; } = string.Empty;

    public int StatusCode { get; private set; }

    public DateTime? ExpiresAt { get; private set; }

    public void SetExpiration(DateTime expiresAt)
    {
        ExpiresAt = expiresAt;
        SetUpdatedAt();
    }
}