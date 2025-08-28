using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products.Create;

public class CreateProductCommandHandler(AppDbContext context)
    : IRequestHandler<CreateProductCommand, ServiceResult<CreateProductResponse>>
{
    public async Task<ServiceResult<CreateProductResponse>> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        bool hasCategory = await context.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
        if (!hasCategory)
            return ServiceResult<CreateProductResponse>.Error(
                "Invalid Category",
                HttpStatusCode.BadRequest,
                $"Category with id {request.CategoryId} does not exist."
            );

        bool hasProduct = await context.Products.AnyAsync(x => x.Name == request.Name, cancellationToken);
        if (hasProduct)
            return ServiceResult<CreateProductResponse>.Error(
                "Product Already Exists",
                HttpStatusCode.BadRequest,
                $"Product with name {request.Name} already exists."
            );


        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            CategoryId = request.CategoryId
        };

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);

        var response = new CreateProductResponse(product.Id);
        return ServiceResult<CreateProductResponse>.SuccessAsCreated(response, $"/api/products/{product.Id}");
    }
}