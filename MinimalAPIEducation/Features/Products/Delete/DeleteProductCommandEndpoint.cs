using MediatR;

namespace MinimalAPIEducation.Features.Products.Delete;

public static class DeleteProductCommandEndpoint
{
    public static RouteGroupBuilder DeleteProductGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:int}", async (int id, IMediator mediator) =>
            {
                await mediator.Send(new DeleteProductCommand(id));
                return Results.NoContent();
            })
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Deletes a product by its ID.");

        return group;
    }
}