using System.Net;
using MediatR;
using MinimalAPIEducation.Repositories;
using MinimalAPIEducation.Shared;

namespace MinimalAPIEducation.Features.Products.Delete;

public class DeleteProductCommandHandler(AppDbContext context) : IRequestHandler<DeleteProductCommand, ServiceResult>
{
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
        return ServiceResult.SuccessAsNoContent();
    }
}