using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIEducation.Extensions;
using MinimalAPIEducation.Filters;

namespace MinimalAPIEducation.Features.Products.Update;

public static class UpdateProductCommandEndpoint
{
    public static RouteGroupBuilder UpdateProductGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/", async ([FromBody] UpdateProductCommand command, IMediator mediator) =>
            (await mediator.Send(command)).ToHttpResult())
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Updates an existing product.")
            .AddEndpointFilter<ValidationFilter<UpdateProductCommand>>() // Validator pipeline
            .MapToApiVersion(1, 0);

        return group;
    }
}