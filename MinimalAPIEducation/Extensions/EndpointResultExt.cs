using System.Net;

namespace MinimalAPIEducation.Extensions;

public static class EndpointResultExt
{
    public static IResult ToHttpResult<T>(this ServiceResult<T> result)
    {
        return result.Status switch
        {
            HttpStatusCode.OK => Results.Ok(result.Data),
            HttpStatusCode.Created => Results.Created(result.UrlAsCreated ?? string.Empty, result.Data),
            HttpStatusCode.BadRequest => Results.BadRequest(result.Fail),
            HttpStatusCode.NotFound => Results.NotFound(result.Fail),
            HttpStatusCode.NoContent => Results.NoContent(),
            _ => Results.Problem(result.Fail?.Detail, statusCode: (int)result.Status)
        };
    }


    public static IResult ToHttpResult(this ServiceResult result)
    {
        return result.Status switch
        {
            HttpStatusCode.NoContent => Results.NoContent(),
            HttpStatusCode.BadRequest => Results.BadRequest(result.Fail),
            HttpStatusCode.NotFound => Results.NotFound(result.Fail),
            _ => Results.Problem(result.Fail?.Detail, statusCode: (int)result.Status)
        };
    }
}