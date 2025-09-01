using System.Net;
using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MinimalAPIEducation.Features.Products.Dtos;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products.GetById;

public class GetProductByIdHandler(AppDbContext context, IDistributedCache cache)
    : IRequestHandler<GetProductByIdQuery, ServiceResult<ProductResponse>>
{
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

    public async Task<ServiceResult<ProductResponse>> Handle(GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        // GetById dinamik bir url oldugu icin boyle tanimliyoruz
        // bu arada ayri ayri cachelerde verileri tutar
        var cacheKey = $"product:{request.Id}";

        var cachedProducts = await cache.GetStringAsync(cacheKey, cancellationToken);

        if (!string.IsNullOrEmpty(cachedProducts))
        {
            var products = JsonSerializer.Deserialize<ProductResponse>(cachedProducts);
            return ServiceResult<ProductResponse>.SuccessAsOk(products!);
        }

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

        var serializedProducts = JsonSerializer.Serialize(productAsDto);

        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheExpiration
        };

        await cache.SetStringAsync(cacheKey, serializedProducts, cacheOptions, cancellationToken);

        return ServiceResult<ProductResponse>.SuccessAsOk(productAsDto);
    }
}