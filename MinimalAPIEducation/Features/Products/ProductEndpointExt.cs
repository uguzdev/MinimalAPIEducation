using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPIEducation.Features.Products.Dtos;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Features.Products;

public static class ProductEndpointExt
{
    public static void ProductEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("/products").WithTags("Products");


        group.MapGet("/", async (AppDbContext context) =>
            {
                var productsDtos = await context.Products
                    .AsNoTracking()
                    .Include(p => p.Category)
                    .Select(p => new ProductResponse(
                        p.Id,
                        p.Name,
                        p.Price,
                        p.CategoryId,
                        p.Category.Name
                    )).ToListAsync();


                return Results.Ok(productsDtos);
            })
            .WithName("GetProducts")
            .Produces<List<ProductResponse>>()
            .WithSummary("Get all products");


        group.MapGet("/{id:int}", async (int id, AppDbContext context) =>
            {
                var productDto = await context.Products
                    .AsNoTracking()
                    .Include(p => p.Category)
                    .Where(p => p.Id == id)
                    .Select(p => new ProductResponse(
                        p.Id,
                        p.Name,
                        p.Price,
                        p.CategoryId,
                        p.Category.Name
                    ))
                    .FirstOrDefaultAsync();

                return productDto is null ? Results.NotFound() : Results.Ok(productDto);
            })
            .WithName("GetProductById")
            .Produces<ProductResponse>()
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Get a product by id");

        group.MapPost("/", async ([FromBody] CreateProductRequest request, AppDbContext context) =>
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                    return Results.BadRequest("Product name cannot be empty.");

                if (request.Price <= 0)
                    return Results.BadRequest("Product price must be greater than zero.");

                var product = new Product
                {
                    Name = request.Name,
                    Price = request.Price,
                    CategoryId = request.CategoryId
                };


                context.Products.Add(product);
                await context.SaveChangesAsync();
                return Results.CreatedAtRoute("GetProductById", new { id = product.Id }, product.Id);
            })
            .WithName("CreateProduct")
            .Produces<int>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Creates a new product.");

        group.MapPut("/{id:int}", async (int id, [FromBody] UpdateProductRequest request, AppDbContext context) =>
            {
                var product = await context.Products.FindAsync(id);
                if (product is null) return Results.NotFound($"Product not found. {id}");

                if (string.IsNullOrWhiteSpace(request.Name))
                    return Results.BadRequest("Product name cannot be empty.");

                if (request.Price <= 0)
                    return Results.BadRequest("Product price must be greater than zero.");

                bool categoryExists = await context.Categories.AnyAsync(c => c.Id == request.CategoryId);
                if (!categoryExists) return Results.BadRequest("Category not found.");

                product.Name = request.Name;
                product.Price = request.Price;
                product.CategoryId = request.CategoryId;
                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Updates an existing product.");

        group.MapDelete("/{id:int}", async (int id, AppDbContext context) =>
            {
                var product = await context.Products.FindAsync(id);
                if (product is null) return Results.NotFound();

                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return Results.NoContent();
            })
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Deletes a product by its ID.");
    }
}