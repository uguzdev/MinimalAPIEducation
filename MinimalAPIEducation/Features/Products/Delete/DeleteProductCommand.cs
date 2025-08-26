using MediatR;
using MinimalAPIEducation.Shared;

namespace MinimalAPIEducation.Features.Products.Delete;

public record DeleteProductCommand(int Id) : IRequest<ServiceResult>;