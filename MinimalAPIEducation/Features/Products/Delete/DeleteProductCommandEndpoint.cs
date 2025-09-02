using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIEducation.Extensions;

namespace MinimalAPIEducation.Features.Products.Delete;

public static class DeleteProductCommandEndpoint
{
    public static RouteGroupBuilder DeleteProductGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:int}", async (int id, IMediator mediator) =>
                (await mediator.Send(new DeleteProductCommand(id))).ToHttpResult())
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Deletes a product by its ID.")
            .MapToApiVersion(1, 0);

        return group;
    }
}