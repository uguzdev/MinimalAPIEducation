using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPIEducation.Features.Products.Delete;

public static class DeleteProductCommandEndpoint
{
    public static RouteGroupBuilder DeleteProductGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:int}", async (int id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteProductCommand(id));

                return result.Status switch
                {
                    HttpStatusCode.NoContent => Results.NoContent(),
                    HttpStatusCode.NotFound => Results.NotFound(result.Fail),
                    _ => Results.Problem(result.Fail?.Detail)
                };
            })
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Deletes a product by its ID.");

        return group;
    }
}