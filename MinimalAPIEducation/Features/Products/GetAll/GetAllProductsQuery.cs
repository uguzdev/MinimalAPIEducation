using MediatR;
using MinimalAPIEducation.Features.Products.Dtos;
using MinimalAPIEducation.Shared;

namespace MinimalAPIEducation.Features.Products.GetAll;

public record GetAllProductsQuery : IRequest<ServiceResult<List<ProductResponse>>>;