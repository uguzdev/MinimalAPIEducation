namespace MinimalAPIEducation.Features.Products.Dtos;

public record UpdateProductRequest(string Name, double Price, int CategoryId);