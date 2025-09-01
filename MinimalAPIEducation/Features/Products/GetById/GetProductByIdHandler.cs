using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAPIEducation.Features.Products.Dtos;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products.GetById;

public class GetProductByIdHandler(AppDbContext context) : IRequestHandler<GetProductByIdQuery, ServiceResult<ProductResponse>>
{
    public async Task<ServiceResult<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productAsDto = await context.Products
            .Include(p => p.Category)
            .Where(p => p.Id == request.Id)
            .Select(p => new ProductResponse(
                p.Id,
                p.Name,
                p.Price,
                p.CategoryId,
                p.Category.Name
            )).FirstOrDefaultAsync(cancellationToken);

        if (productAsDto == null)
            return ServiceResult<ProductResponse>.Error(
                "Product Not Found",
                HttpStatusCode.NotFound,
                $"No product found with id {request.Id}"
            );

        return ServiceResult<ProductResponse>.SuccessAsOk(productAsDto);
    }
}