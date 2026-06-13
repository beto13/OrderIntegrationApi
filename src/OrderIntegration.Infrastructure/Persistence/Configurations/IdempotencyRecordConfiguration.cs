using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderIntegration.Domain.Entities;

namespace OrderIntegration.Infrastructure.Persistence.Configurations;

public class IdempotencyRecordConfiguration : IEntityTypeConfiguration<IdempotencyRecord>
{
    public void Configure(EntityTypeBuilder<IdempotencyRecord> builder)
    {
        builder.ToTable("IdempotencyRecords");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.RequestHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.ResponseBody)
            .IsRequired();

        builder.Property(x => x.StatusCode)
            .IsRequired();

        builder.Property(x => x.ExpiresAt);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.Key)
            .IsUnique();
    }
}