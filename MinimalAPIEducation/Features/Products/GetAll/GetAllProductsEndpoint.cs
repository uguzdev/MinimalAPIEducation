using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIEducation.Extensions;
using MinimalAPIEducation.Features.Products.Dtos;

namespace MinimalAPIEducation.Features.Products.GetAll;

public static class GetAllProductsEndpoint
{
    public static RouteGroupBuilder GetAllPorudctsGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetAllProductsQuery())).ToHttpResult())
            .WithName("GetAllProducts")
            .Produces<List<ProductResponse>>()
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all products")
            .MapToApiVersion(1, 0);

        return group;
    }
}