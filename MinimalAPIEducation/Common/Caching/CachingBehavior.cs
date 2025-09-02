using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace MinimalAPIEducation.Common.Caching;

public class CachingBehavior<TRequest, TResponse>(IDistributedCache cache) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not ICacheable cacheable) return await next(cancellationToken);

        var cachedJson = await cache.GetStringAsync(cacheable.CacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedJson))
        {
            var cached = JsonSerializer.Deserialize<TResponse>(cachedJson);
            if (cached is not null)
                return cached;
        }

        var response = await next(cancellationToken);

        if (response is not ServiceResult { IsSuccess: true }) return response;
        var data = JsonSerializer.Serialize(response);
        var options = new DistributedCacheEntryOptions();
        if (cacheable.Expiration.HasValue)
            options.AbsoluteExpirationRelativeToNow = cacheable.Expiration;

        await cache.SetStringAsync(cacheable.CacheKey, data, options, cancellationToken);

        return response;
    }
}