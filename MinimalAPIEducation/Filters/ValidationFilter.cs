using FluentValidation;

namespace MinimalAPIEducation.Filters;

public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator is null) return await next(context);

        var requestModel = context.Arguments.OfType<T>().FirstOrDefault();
        if (requestModel is null) return await next(context);

        var validateResult = await validator.ValidateAsync(requestModel);
        if (!validateResult.IsValid) return Results.ValidationProblem(validateResult.ToDictionary());

        return await next(context);
    }
}