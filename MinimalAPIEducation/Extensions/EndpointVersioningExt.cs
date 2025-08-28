using Asp.Versioning;
using Asp.Versioning.Builder;

namespace MinimalAPIEducation.Extensions;

public static class EndpointVersioningExt
{
    public static IServiceCollection AddVersioningExt(this IServiceCollection services,
        Action<ApiVersioningOptions>? configureVersioning = null)
    {
        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    public static ApiVersionSet AddVersionSetExt(this WebApplication app)
    {
        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .HasApiVersion(new ApiVersion(1, 2))
            .ReportApiVersions()
            .Build();
        return apiVersionSet;
    }
}