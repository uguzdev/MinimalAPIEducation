using MediatR;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products.Delete;

public class DeleteProductCommandHandler(AppDbContext context) : IRequestHandler<DeleteProductCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var hasProduct = await context.Products.FindAsync([request.Id], cancellationToken);
        if (hasProduct is null) throw new KeyNotFoundException("Product with the given ID not found.");

        context.Products.Remove(hasProduct);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}