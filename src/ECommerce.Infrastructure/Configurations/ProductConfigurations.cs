using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
             new Product
             {
                 Id = 1,
                 Name = "Smartphone",
                 Description = "Latest model smartphone with advanced features.",
                 Price = 699.99m,
                 StockQuantity = 50,
                 ImageUrl = "https://example.com/images/smartphone.jpg",
                 DiscountPercentage = 10,
                 CategoryId = 1,
                 IsAvailable = true
             },
             new Product
             {
                 Id = 2,
                 Name = "Laptop",
                 Description = "High-performance laptop suitable for all your needs.",
                 Price = 999.99m,
                 StockQuantity = 30,
                 ImageUrl = "https://example.com/images/laptop.jpg",
                 DiscountPercentage = 15,
                 CategoryId = 1,
                 IsAvailable = true
             },
             new Product
             {
                 Id = 3,
                 Name = "Science Fiction Novel",
                 Description = "A thrilling science fiction novel set in the future.",
                 Price = 19.99m,
                 StockQuantity = 100,
                 ImageUrl = "https://example.com/images/scifi-novel.jpg",
                 DiscountPercentage = 5,
                 CategoryId = 2,
                 IsAvailable = true
             }
         );
    }
}

