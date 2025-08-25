using MediatR;
using MinimalAPIEducation.Features.Products.Dtos;

namespace MinimalAPIEducation.Features.Products.GetById;

public static class GetProductByIdEndpoint
{
    public static RouteGroupBuilder GetByIdProductGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:int}", async (int id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetProductByIdQuery(id));
                return Results.Ok(result);
            })
            .WithName("GetProductById")
            .Produces<ProductResponse>()
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Get a product by id");

        return group;
    }
}