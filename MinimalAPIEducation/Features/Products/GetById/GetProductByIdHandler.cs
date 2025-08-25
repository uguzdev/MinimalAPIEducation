using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAPIEducation.Features.Products.Dtos;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products.GetById;

public class GetProductByIdHandler(AppDbContext context) : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productDto = await context.Products
            .Include(p => p.Category)
            .Where(p => p.Id == request.Id)
            .Select(p => new ProductResponse(
                p.Id,
                p.Name,
                p.Price,
                p.CategoryId,
                p.Category.Name
            )).FirstOrDefaultAsync(cancellationToken);

        return productDto!;
    }
}