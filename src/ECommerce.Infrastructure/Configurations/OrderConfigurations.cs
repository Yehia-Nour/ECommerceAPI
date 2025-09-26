using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(o => o.BillingAddress)
            .WithMany()
            .HasForeignKey(o => o.BillingAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.ShippingAddress)
           .WithMany()
           .HasForeignKey(o => o.ShippingAddressId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Cancellation)
            .WithOne(c => c.Order)
            .HasForeignKey<Cancellation>(c => c.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(o => o.OrderNumber)
            .HasMaxLength(30);

        builder.Property(o => o.TotalBaseAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.TotalDiscountAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.ShippingCost)
            .HasColumnType("decimal(18,2)");

        builder.Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,2)");
    }
}
