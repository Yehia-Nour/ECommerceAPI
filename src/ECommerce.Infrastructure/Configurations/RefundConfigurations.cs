using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations;

public class RefundConfigurations : IEntityTypeConfiguration<Refund>
{
    public void Configure(EntityTypeBuilder<Refund> builder)
    {
        builder.HasOne(r => r.Payment)
            .WithOne(p => p.Refund)
            .HasForeignKey<Refund>(r => r.PaymentId);

        builder.HasOne(r => r.Cancellation)
            .WithOne(c => c.Refund)
            .HasForeignKey<Refund>(r => r.CancellationId);

        builder.Property(r => r.Amount)
            .HasColumnType("decimal(18,2)");

        builder.Property(r => r.RefundReason)
            .HasMaxLength(500);

        builder.Property(r => r.TransactionId)
            .HasMaxLength(100);
    }
}
