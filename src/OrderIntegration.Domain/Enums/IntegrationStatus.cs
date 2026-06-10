namespace OrderIntegration.Domain.Enums;

public enum IntegrationStatus
{
    Pending = 1,
    Processing = 2,
    Synced = 3,
    Failed = 4,
    Retrying = 5
}