using MediatR;
using MinimalAPIEducation.Features.Products.Dtos;

namespace MinimalAPIEducation.Features.Products.GetAll;

public record GetAllProductsQuery : IRequest<List<ProductResponse>>;