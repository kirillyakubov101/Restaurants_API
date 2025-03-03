using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extenstions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));

        // Scans the assembly for classes that inherit from Profile and registers them for AutoMapper.
        services.AddAutoMapper(applicationAssembly);
        // Scans the assembly for classes that inherit from AbstractValidator and registers them for FluentValidator
        services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();

        //register the Current HTTP Context | I ensure that the IHttpContextAccessor service is registered and available for dependency injection
        services.AddHttpContextAccessor(); 
    }
}
