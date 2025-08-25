using Microsoft.EntityFrameworkCore;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Extensions;

public static class AppExt
{
    public static IServiceCollection AddExtension(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

        return service;
    }
}