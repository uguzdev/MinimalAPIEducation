using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIEducation.Filters;

namespace MinimalAPIEducation.Features.Products.Create;

public static class CreateProductCommandEndpoint
{
    public static RouteGroupBuilder CreateProductGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async ([FromBody] CreateProductCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Created($"/products/{result.Id}", result.Id);
            })
            .WithName("CreateProduct")
            .Produces<int>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Creates a new product.")
            .AddEndpointFilter<ValidationFilter<CreateProductCommand>>(); // Validator pipeline

        return group;
    }
}