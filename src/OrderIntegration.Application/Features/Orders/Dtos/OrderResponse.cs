namespace OrderIntegration.Application.Features.Orders.Dtos;

public class OrderResponse
{
    public Guid Id { get; set; }

    public string ExternalOrderId { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public string Currency { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public string IntegrationStatus { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public List<OrderItemResponse> Items { get; set; } = new();
}