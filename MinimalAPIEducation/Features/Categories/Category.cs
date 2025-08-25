using MinimalAPIEducation.Features.Products;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Categories;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<Product>? Products { get; set; }
}