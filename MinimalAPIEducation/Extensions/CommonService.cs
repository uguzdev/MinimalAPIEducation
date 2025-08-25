using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MinimalAPIEducation.Repositories;

namespace MinimalAPIEducation.Extensions;

public static class CommonService
{
    public static IServiceCollection AddCommonServiceExt(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlServer")));


        service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        service.AddFluentValidationAutoValidation();
        service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return service;
    }
}