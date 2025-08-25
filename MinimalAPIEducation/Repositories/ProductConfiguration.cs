using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinimalAPIEducation.Features.Products;

namespace MinimalAPIEducation.Repositories;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");


        builder.HasData(
            new Product { Id = 1, Name = "Nvidia 50070", Price = 33.000, CategoryId = 1 },
            new Product { Id = 2, Name = "Nvidia 50090", Price = 109.000, CategoryId = 1 },
            new Product { Id = 3, Name = "AMD 7990x", Price = 10.000, CategoryId = 2 }
        );
    }
}