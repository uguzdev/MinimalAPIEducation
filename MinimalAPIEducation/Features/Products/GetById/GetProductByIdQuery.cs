using MediatR;
using MinimalAPIEducation.Features.Products.Dtos;

namespace MinimalAPIEducation.Features.Products.GetById;

public record GetProductByIdQuery(int Id) : IRequest<ProductResponse>;