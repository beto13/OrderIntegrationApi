using Microsoft.EntityFrameworkCore;
using OrderIntegration.Domain.Entities;

namespace OrderIntegration.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Order> Orders { get; }

    DbSet<OrderItem> OrderItems { get; }

    DbSet<IntegrationEvent> IntegrationEvents { get; }

    DbSet<WebhookEvent> WebhookEvents { get; }

    DbSet<IdempotencyRecord> IdempotencyRecords { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}