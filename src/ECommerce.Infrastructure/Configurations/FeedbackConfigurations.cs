using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations;

public class FeedbackConfigurations : IEntityTypeConfiguration<Feedback>
{
    public void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.HasOne(f => f.Customer)
            .WithMany(c => c.Feedbacks)
            .HasForeignKey(f => f.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.Product)
            .WithMany(p => p.Feedbacks)
            .HasForeignKey(f => f.ProductId)
            .OnDelete(DeleteBehavior.Restrict); ;
    }
}

