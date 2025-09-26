using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasMany(c => c.Addresses)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Feedbacks)
            .WithOne(f => f.Customer)
            .HasForeignKey(f => f.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Carts)
            .WithOne(cr => cr.Customer)
            .HasForeignKey(cr => cr.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);



        builder.HasIndex(c => c.Email)
            .IsUnique()
            .HasDatabaseName("IX_Email_Unique");

        builder.Property(c => c.FirstName)
        .IsRequired()
        .HasColumnType("nvarchar(50)");

        builder.Property(c => c.LastName)
        .IsRequired()
        .HasColumnType("nvarchar(50)");
    }
}

