using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations;

public class CancellationConfigurations : IEntityTypeConfiguration<Cancellation>
{
    public void Configure(EntityTypeBuilder<Cancellation> builder)
    {
        builder.Property(c => c.Reason)
            .HasMaxLength(500);

        builder.Property(c => c.Remarks)
            .HasMaxLength(500);

        builder.Property(c => c.OrderAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.CancellationCharges)
            .HasColumnType("decimal(18,2)");
    }
}

