using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIEducation.Filters;

namespace MinimalAPIEducation.Features.Products.Update;

public static class UpdateProductCommandEndpoint
{
    public static RouteGroupBuilder UpdateProductGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/", async ([FromBody] UpdateProductCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);

                return result.Status switch
                {
                    HttpStatusCode.NoContent => Results.NoContent(),
                    HttpStatusCode.NotFound => Results.NotFound(result.Fail),
                    HttpStatusCode.BadRequest => Results.BadRequest(result.Fail),
                    _ => Results.Problem(result.Fail?.Detail)
                };
            })
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Updates an existing product.")
            .AddEndpointFilter<ValidationFilter<UpdateProductCommand>>(); // Validator pipeline

        return group;
    }
}