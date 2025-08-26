using MediatR;
using MinimalAPIEducation.Features.Products.Dtos;
using MinimalAPIEducation.Shared;

namespace MinimalAPIEducation.Features.Products.GetById;

public record GetProductByIdQuery(int Id) : IRequest<ServiceResult<ProductResponse>>;