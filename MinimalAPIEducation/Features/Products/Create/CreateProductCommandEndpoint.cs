using System.Net;
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

                return result.Status switch
                {
                    HttpStatusCode.Created => Results.Created(result.UrlAsCreated!, result.Data),
                    HttpStatusCode.BadRequest => Results.BadRequest(result.Fail),
                    _ => Results.Problem(result.Fail?.Detail)
                };
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Creates a new product.")
            .AddEndpointFilter<ValidationFilter<CreateProductCommand>>(); // Validator pipeline

        return group;
    }
}