using Restaurants.Infrastructure.Extenstions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extenstions;
using Serilog;
using Restaurants.API.Middlewares;
using Restaurants.Domain.Entities;
using Restaurants.API.Extenstions;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Add Seeder - some values to work with and test the API
    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
    await seeder.Seed();

    app.UseMiddleware<ErrorHandlingMiddleware>(); //the error handling middleware
    app.UseMiddleware<RequestTimeLoggingMiddleware>(); //Timer Middleware

    // Configure the HTTP request pipeline.
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseHttpsRedirection();

    app.UseAuthorization();

    //Use the identity framework endpoints
    app.MapGroup("api/identity")
        .WithTags("Identity")
        .MapIdentityApi<User>();

    app.MapControllers();

    //run the program
    app.Run();
}

catch(Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }
