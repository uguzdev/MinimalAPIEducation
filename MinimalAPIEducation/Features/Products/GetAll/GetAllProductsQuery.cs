using MediatR;
using MinimalAPIEducation.Common.Caching;
using MinimalAPIEducation.Features.Products.Dtos;

namespace MinimalAPIEducation.Features.Products.GetAll;

public class GetAllProductsQuery : IRequest<ServiceResult<List<ProductResponse>>>, ICacheable
{
    public string CacheKey => CacheSettings.Keys.ProductsAll;
    public TimeSpan? Expiration => CacheSettings.Durations.Short;
}