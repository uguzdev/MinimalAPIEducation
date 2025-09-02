using MediatR;

namespace MinimalAPIEducation.Features.Products.Update;

public record UpdateProductCommand(int Id, string Name, double Price, int CategoryId) : IRequest<ServiceResult>;