namespace MinimalAPIEducation.Features.Products.Dtos;

public record ProductResponse(int Id, string Name, double Price, int CategoryId, string CategoryName);

// System.Text.Json.JsonException: A possible object cycle was detected
// direk entity donemedik