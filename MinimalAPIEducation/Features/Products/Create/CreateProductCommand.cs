using MediatR;

namespace MinimalAPIEducation.Features.Products.Create;

public record CreateProductCommand(
    string Name,
    double Price,
    int CategoryId
) : IRequest<ServiceResult<CreateProductResponse>>;