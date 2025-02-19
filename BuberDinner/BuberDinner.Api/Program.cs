using BuberDinner.Application;
using BuberDinner.Filters;
using BuberDinner.Infrastructure;
using BuberDinner.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddApplication() 
                    .AddInfrastructure(builder.Configuration);

    // builder.Services.AddControllers(options => 
    //     options.Filters.Add<ErrorHandlingFilterAttribute>()
    //     );
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    //app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}