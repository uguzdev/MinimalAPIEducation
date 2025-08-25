using MediatR;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products.Create;

public class CreateProductCommandHandler(AppDbContext context)
    : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            CategoryId = request.CategoryId
        };

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);

        return new CreateProductResponse(product.Id);
    }
}