using MinimalAPIEducation.Features.Categories;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; init; } = null!;
}