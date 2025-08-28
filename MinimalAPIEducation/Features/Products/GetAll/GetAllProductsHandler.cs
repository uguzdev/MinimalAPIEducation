using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAPIEducation.Features.Products.Dtos;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products.GetAll;

public class GetAllProductsHandler(AppDbContext context)
    : IRequestHandler<GetAllProductsQuery, ServiceResult<List<ProductResponse>>>
{
    public async Task<ServiceResult<List<ProductResponse>>> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Select(p => new ProductResponse(
                p.Id,
                p.Name,
                p.Price,
                p.CategoryId,
                p.Category.Name
            )).ToListAsync(cancellationToken);

        if (!products.Any())
            return ServiceResult<List<ProductResponse>>.Error("No products found", HttpStatusCode.NotFound);

        return ServiceResult<List<ProductResponse>>.SuccessAsOk(products);
    }
}