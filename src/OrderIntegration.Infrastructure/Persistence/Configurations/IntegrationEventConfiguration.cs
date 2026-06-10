using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderIntegration.Domain.Entities;

namespace OrderIntegration.Infrastructure.Persistence.Configurations;

public class IntegrationEventConfiguration : IEntityTypeConfiguration<IntegrationEvent>
{
    public void Configure(EntityTypeBuilder<IntegrationEvent> builder)
    {
        builder.ToTable("IntegrationEvents");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderId)
            .IsRequired();

        builder.Property(x => x.EventType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Payload)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.ErrorMessage)
            .HasMaxLength(1000);

        builder.Property(x => x.ProcessedAt);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.OrderId);
    }
}