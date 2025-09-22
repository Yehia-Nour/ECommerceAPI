using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
           new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and accessories", IsActive = true },
           new Category { Id = 2, Name = "Books", Description = "Books and magazines", IsActive = true }
       );
    }
}

