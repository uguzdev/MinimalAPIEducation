using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIEducation.Features.Products.Dtos;

namespace MinimalAPIEducation.Features.Products.GetById;

public static class GetProductByIdEndpoint
{
    public static RouteGroupBuilder GetByIdProductGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:int}", async (int id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetProductByIdQuery(id));

                return result.Status switch
                {
                    HttpStatusCode.OK => Results.Ok(result.Data),
                    HttpStatusCode.NotFound => Results.NotFound(result.Fail),
                    _ => Results.Problem(result.Fail?.Detail)
                };
            })
            .WithName("GetProductById")
            .Produces<ProductResponse>()
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Get a product by id");

        return group;
    }
}