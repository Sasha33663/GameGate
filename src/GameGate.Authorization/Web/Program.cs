using Application;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.  //TODO: удалить комментарий
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<UserService>();
        builder.Services.AddDbContext<DatabaseContext>((options) => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")), ServiceLifetime.Transient);
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddIdentityApiEndpoints<User>(options =>

        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders(); // Essential for authentication  //TODO: удалить комментарий

        var app = builder.Build();
       
        app.UseSwagger(c => c.RouteTemplate = "swagger/{documentname}/swagger.json")
     .UseSwaggerUI(c =>
     {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "Musbooking-Dev");
     });
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}