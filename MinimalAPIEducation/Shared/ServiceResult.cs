using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPIEducation.Shared;

public class ServiceResult
{
    [JsonIgnore] public HttpStatusCode Status { get; set; }
    public ProblemDetails? Fail { get; set; }
    [JsonIgnore] public bool IsSuccess => Fail == null;
    [JsonIgnore] public bool IsFail => !IsSuccess;

    // Static factory methods for non-generic results
    public static ServiceResult SuccessAsNoContent()
    {
        return new ServiceResult
        {
            Status = HttpStatusCode.NoContent
        };
    }

    public static ServiceResult Error(string title, HttpStatusCode status, string? detail = null,
        IDictionary<string, object?>? extensions = null)
    {
        return new ServiceResult
        {
            Status = status,
            Fail = new ProblemDetails
            {
                Title = title,
                Detail = detail,
                Status = (int)status,
                Extensions = extensions
            }
        };
    }

    public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
    {
        return Error(
            "Validation errors occurred",
            HttpStatusCode.BadRequest,
            "Please check the errors property for more details",
            errors
        );
    }
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; set; }
    [JsonIgnore] public string? UrlAsCreated { get; set; }

    // Static factory methods for generic results
    public static ServiceResult<T> SuccessAsOk(T data)
    {
        return new ServiceResult<T>
        {
            Data = data,
            Status = HttpStatusCode.OK
        };
    }

    public static ServiceResult<T> SuccessAsCreated(T data, string url)
    {
        return new ServiceResult<T>
        {
            Data = data,
            Status = HttpStatusCode.Created,
            UrlAsCreated = url
        };
    }

    public new static ServiceResult<T> Error(string title, HttpStatusCode status, string? detail = null,
        IDictionary<string, object?>? extensions = null)
    {
        return new ServiceResult<T>
        {
            Status = status,
            Fail = new ProblemDetails
            {
                Title = title,
                Detail = detail,
                Status = (int)status,
                Extensions = extensions
            }
        };
    }

    public new static ServiceResult<T> ErrorFromValidation(IDictionary<string, object?> errors)
    {
        return Error(
            "Validation errors occurred",
            HttpStatusCode.BadRequest,
            "Please check the errors property for more details",
            errors
        );
    }
}