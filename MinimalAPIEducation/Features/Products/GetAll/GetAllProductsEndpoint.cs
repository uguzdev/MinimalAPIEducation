using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIEducation.Features.Products.Dtos;

namespace MinimalAPIEducation.Features.Products.GetAll;

public static class GetAllProductsEndpoint
{
    public static RouteGroupBuilder GetAllPorudctsGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllProductsQuery());

                return result.Status switch
                {
                    HttpStatusCode.OK => Results.Ok(result.Data),
                    HttpStatusCode.NotFound => Results.NotFound(result.Fail),
                    _ => Results.Problem(result.Fail?.Detail)
                };
            })
            .WithName("GetAllProducts")
            .Produces<List<ProductResponse>>()
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all products");

        return group;
    }
}