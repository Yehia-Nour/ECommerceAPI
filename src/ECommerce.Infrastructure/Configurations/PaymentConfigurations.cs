using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations;

public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(p => p.PaymentMethod)
            .HasMaxLength(50);

        builder.Property(p => p.TransactionId)
            .HasMaxLength(50);

        builder.Property(p => p.Status)
            .HasMaxLength(20);

        builder.Property(p => p.Amount)
            .HasColumnType("decimal(18,2)");
    }
}

