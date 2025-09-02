using MediatR;
using MinimalAPIEducation.Common.Caching;
using MinimalAPIEducation.Extensions;
using MinimalAPIEducation.Features.Products;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCommonServiceExt(builder.Configuration);
builder.Services.AddVersioningExt();

// Redis
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = "localhost:6379"; });
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

// OpenTelemetry + Jaeger
builder.Services.AddOpenTelemetry().WithTracing(traceProvider =>
{
    traceProvider.ConfigureResource(resource =>
    {
        resource.AddService(
            "minimal-api-education",
            serviceVersion: "1.0.0"
        );
    });

    traceProvider.AddAspNetCoreInstrumentation();
    traceProvider.AddEntityFrameworkCoreInstrumentation(x =>
    {
        x.SetDbStatementForStoredProcedure = true;
        x.SetDbStatementForText = true;
    });
    traceProvider.AddConsoleExporter(); // console exporter (consolda trace verilerinin gosterilmesini saglar)

    traceProvider.AddOtlpExporter(x => // jaeger exporter (jaeger servera trace verilerinin gonderilmesini saglar)
    {
        x.Endpoint = new Uri("http://localhost:4318/v1/traces");
        x.Protocol = OtlpExportProtocol.HttpProtobuf;
    });
});

var app = builder.Build();

// Group endpoints
app.AddProductGroupEndpointExt(app.AddVersionSetExt());

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}


app.Run();