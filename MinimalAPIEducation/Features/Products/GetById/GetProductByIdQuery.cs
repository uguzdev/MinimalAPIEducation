using MediatR;
using MinimalAPIEducation.Common.Caching;
using MinimalAPIEducation.Features.Products.Dtos;

namespace MinimalAPIEducation.Features.Products.GetById;

public class GetProductByIdQuery(int id) : IRequest<ServiceResult<ProductResponse>>, ICacheable
{
    public int Id { get; } = id;

    public string CacheKey => CacheSettings.Keys.Product(Id);
    public TimeSpan? Expiration => CacheSettings.Durations.Short;
}