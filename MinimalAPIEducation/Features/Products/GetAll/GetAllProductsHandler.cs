using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAPIEducation.Features.Products.Dtos;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products.GetAll;

public class GetAllProductsHandler(AppDbContext context) : IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
{
    public async Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
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
            ))
            .ToListAsync(cancellationToken);

        return products;
    }
}