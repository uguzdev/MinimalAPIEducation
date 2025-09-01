using System.Net;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products.Delete;

public class DeleteProductCommandHandler(AppDbContext context, IDistributedCache cache)
    : IRequestHandler<DeleteProductCommand, ServiceResult>
{
    private const string AllProductsCacheKey = "products:all";

    public async Task<ServiceResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var hasProduct = await context.Products.FindAsync([request.Id], cancellationToken);
        if (hasProduct is null)
            return ServiceResult.Error(
                "Not Found",
                HttpStatusCode.NotFound,
                $"Product with id {request.Id} not found."
            );

        context.Products.Remove(hasProduct);
        await context.SaveChangesAsync(cancellationToken);

        // Cache temizleme
        var productCacheKey = $"product:{request.Id}";
        await cache.RemoveAsync(AllProductsCacheKey, cancellationToken);
        await cache.RemoveAsync(productCacheKey, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}