using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations;

public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasOne(p => p.Refund)
            .WithOne(r => r.Payment)
            .HasForeignKey<Refund>(r => r.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

