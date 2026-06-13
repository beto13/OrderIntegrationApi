using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderIntegration.Domain.Entities;

namespace OrderIntegration.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExternalOrderId)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.ExternalOrderId)
            .IsUnique();

        builder.Property(x => x.CustomerName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.CustomerEmail)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.TotalAmount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.IntegrationStatus)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Items)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}