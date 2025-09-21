using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations;
public class StatusConfigurations : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasData(
            new Status { Id = 1, Name = "Pending" },
            new Status { Id = 2, Name = "Processing" },
            new Status { Id = 3, Name = "Shipped" },
            new Status { Id = 4, Name = "Delivered" },
            new Status { Id = 5, Name = "Canceled" },
            new Status { Id = 6, Name = "Completed" },
            new Status { Id = 7, Name = "Failed" },
            new Status { Id = 8, Name = "Approved" },
            new Status { Id = 9, Name = "Rejected" },
            new Status { Id = 10, Name = "Refunded" });
    }
}
