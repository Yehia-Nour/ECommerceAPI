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
    }
}
