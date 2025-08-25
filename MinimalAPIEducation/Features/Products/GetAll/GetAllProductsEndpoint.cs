using MediatR;
using MinimalAPIEducation.Features.Products.Dtos;

namespace MinimalAPIEducation.Features.Products.GetAll;

public static class GetAllProductsEndpoint
{
    public static RouteGroupBuilder GetAllPorudctsGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllProductsQuery());
                return Results.Ok(result);
            })
            .WithName("GetAllProducts")
            .Produces<List<ProductResponse>>()
            .WithSummary("Get all products");

        return group;
    }
}