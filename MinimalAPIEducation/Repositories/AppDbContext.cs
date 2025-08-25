using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MinimalAPIEducation.Features.Categories;
using MinimalAPIEducation.Features.Products;

namespace MinimalAPIEducation.Repositories;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}