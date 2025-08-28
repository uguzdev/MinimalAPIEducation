using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIEducation.Extensions;
using MinimalAPIEducation.Features.Products.Dtos;

namespace MinimalAPIEducation.Features.Products.GetById;

public static class GetProductByIdEndpoint
{
    public static RouteGroupBuilder GetByIdProductGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:int}", async (int id, IMediator mediator) =>
                (await mediator.Send(new GetProductByIdQuery(id))).ToHttpResult())
            .WithName("GetProductById")
            .Produces<ProductResponse>()
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Get a product by id")
            .MapToApiVersion(1, 0);

        return group;
    }
}