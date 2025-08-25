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
                await mediator.Send(command);
                return Results.NoContent();
            })
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Updates an existing product.")
            .AddEndpointFilter<ValidationFilter<UpdateProductCommand>>(); // Validator pipeline

        return group;
    }
}