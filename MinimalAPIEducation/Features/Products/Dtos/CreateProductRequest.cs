namespace MinimalAPIEducation.Features.Products.Dtos;

public record CreateProductRequest(string Name, double Price, int CategoryId);