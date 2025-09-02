using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIEducation.Extensions;
using MinimalAPIEducation.Filters;

namespace MinimalAPIEducation.Features.Products.Create;

public static class CreateProductCommandEndpoint
{
    public static RouteGroupBuilder CreateProductGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async ([FromBody] CreateProductCommand command, IMediator mediator) =>
            (await mediator.Send(command)).ToHttpResult())
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Creates a new product.")
            .AddEndpointFilter<ValidationFilter<CreateProductCommand>>() // Validator pipeline
            .MapToApiVersion(1, 0);

        return group;
    }
}