using Asp.Versioning.Builder;
using MinimalAPIEducation.Features.Products.Create;
using MinimalAPIEducation.Features.Products.Delete;
using MinimalAPIEducation.Features.Products.GetAll;
using MinimalAPIEducation.Features.Products.GetById;
using MinimalAPIEducation.Features.Products.Update;

namespace MinimalAPIEducation.Features.Products;

public static class ProductEndpointExt
{
    public static void AddProductGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/products")
            .WithTags("Products")
            .WithApiVersionSet(apiVersionSet)
            .GetAllPorudctsGroupItemEndpoint()
            .GetByIdProductGroupItemEndpoint()
            .CreateProductGroupItemEndpoint()
            .UpdateProductGroupItemEndpoint()
            .DeleteProductGroupItemEndpoint();
    }
}