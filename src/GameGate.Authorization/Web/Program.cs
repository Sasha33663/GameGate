using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Infrastructure;
using Application;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace web;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<UserService>();
        builder.Services.AddDbContext<DatabaseContext>((options) => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddIdentityApiEndpoints<User>(options =>

        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders(); // Essential for authentication

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
