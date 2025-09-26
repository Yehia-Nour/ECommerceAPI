using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations;

public class AddressConfigurations : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(a => a.AddressLine1)
            .HasMaxLength(100);

        builder.Property(a => a.AddressLine2)
            .HasMaxLength(100);

        builder.Property(a => a.City)
            .HasMaxLength(50);

        builder.Property(a => a.State)
            .HasMaxLength(50);

        builder.Property(a => a.Country)
            .HasMaxLength(50);
    }
}
