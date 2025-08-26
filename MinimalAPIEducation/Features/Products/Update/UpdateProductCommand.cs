using MediatR;
using MinimalAPIEducation.Shared;

namespace MinimalAPIEducation.Features.Products.Update;

public record UpdateProductCommand(int Id, string Name, double Price, int CategoryId) : IRequest<ServiceResult>;