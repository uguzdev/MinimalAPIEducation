using System.Net;
using System.Text.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MinimalAPIEducation.Features.Products.Dtos;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products.GetAll;

public class GetAllProductsHandler(AppDbContext context, IDistributedCache cache)
    : IRequestHandler<GetAllProductsQuery, ServiceResult<List<ProductResponse>>>
{
    private const string CacheKey = "products:all";
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);


    public async Task<ServiceResult<List<ProductResponse>>> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        // Önce cache'den kontrol et
        var cachedProducts = await cache.GetStringAsync(CacheKey, cancellationToken);

        // cachedProducts boş veya null değilse burası çalışır
        if (!string.IsNullOrEmpty(cachedProducts))
        {
            var products = JsonSerializer.Deserialize<List<ProductResponse>>(cachedProducts);
            return ServiceResult<List<ProductResponse>>.SuccessAsOk(products!);
        }

        // Cache'de yoksa veritabanından al
        var productsFromDb = await context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Select(p => new ProductResponse(
                p.Id,
                p.Name,
                p.Price,
                p.CategoryId,
                p.Category.Name
            )).ToListAsync(cancellationToken);

        if (!productsFromDb.Any())
            return ServiceResult<List<ProductResponse>>.Error("No products found", HttpStatusCode.NotFound);

        // Cache'e kaydet
        var serializedProducts = JsonSerializer.Serialize(productsFromDb);

        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheExpiration
        };

        await cache.SetStringAsync(CacheKey, serializedProducts, cacheOptions, cancellationToken);

        return ServiceResult<List<ProductResponse>>.SuccessAsOk(productsFromDb);
    }
}