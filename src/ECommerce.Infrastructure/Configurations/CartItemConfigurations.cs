using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations;

public class CartItemConfigurations : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(ci => ci.UnitPrice)
            .HasColumnType("decimal(18,2)");

        builder.Property(ci => ci.Discount)
            .HasColumnType("decimal(18,2)");

        builder.Property(ci => ci.TotalPrice)
            .HasColumnType("decimal(18,2)");
    }
}