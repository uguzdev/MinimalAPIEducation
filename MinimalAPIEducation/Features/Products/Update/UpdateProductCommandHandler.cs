using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAPIEducation.Features.Products.Create;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products.Update;

public class UpdateProductCommandHandler(AppDbContext context) : IRequestHandler<UpdateProductCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        bool hasCategory = await context.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
        if (!hasCategory)
            return ServiceResult<CreateProductResponse>.Error(
                "Invalid Category",
                HttpStatusCode.BadRequest,
                $"Category with id {request.CategoryId} does not exist."
            );

        var hasProduct = await context.Products.FindAsync([request.Id], cancellationToken);
        if (hasProduct is null)
            return ServiceResult.Error(
                "Not Found",
                HttpStatusCode.NotFound,
                $"Product with id {request.Id} not found."
            );

        bool duplicateExists = await context.Products
            .AnyAsync(p => p.Id != request.Id && p.Name == request.Name && p.CategoryId == request.CategoryId,
                cancellationToken);

        if (duplicateExists)
            return ServiceResult.Error(
                "Duplicate Product",
                HttpStatusCode.BadRequest,
                $"Another product with name '{request.Name}' already exists in this category."
            );


        hasProduct.Name = request.Name;
        hasProduct.Price = request.Price;
        hasProduct.CategoryId = request.CategoryId;

        await context.SaveChangesAsync(cancellationToken);
        return ServiceResult.SuccessAsNoContent();
    }
}