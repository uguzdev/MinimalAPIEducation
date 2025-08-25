using MinimalAPIEducation.Features.Categories;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products;

public class Product : BaseEntity
{
    public string Name { get; init; } = string.Empty;
    public double Price { get; init; }

    public int CategoryId { get; set; }
    public Category Category { get; init; } = null!;
}