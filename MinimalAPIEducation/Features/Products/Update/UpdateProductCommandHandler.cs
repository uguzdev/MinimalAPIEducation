using MediatR;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products.Update;

public class UpdateProductCommandHandler(AppDbContext context) : IRequestHandler<UpdateProductCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var hasProduct = await context.Products.FindAsync([request.Id], cancellationToken);

        if (hasProduct is null) throw new KeyNotFoundException("Product with the given ID not found.");

        hasProduct.Name = request.Name;
        hasProduct.Price = request.Price;
        hasProduct.CategoryId = request.CategoryId;

        await context.SaveChangesAsync(cancellationToken);

        // İşlem başarılı olduğunda Unit.Value döndürün
        return Unit.Value;
    }
}

// => bos donus icin Unit.Value kullanilir