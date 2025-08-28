using MinimalAPIEducation.Extensions;
using MinimalAPIEducation.Features.Products;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCommonServiceExt(builder.Configuration);
builder.Services.AddVersioningExt();

var app = builder.Build();

// Group endpoints
app.AddProductGroupEndpointExt(app.AddVersionSetExt());

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}


app.Run();