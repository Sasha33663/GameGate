
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
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<UserService>();
        builder.Services.AddDbContext<DatabaseContext>((options) => options.UseNpgsql("Server=localhost;Port=5432;Database=GameGate.Authorization; UserId=postgres;Password=Batonbatonbaton123;"));
        builder.Services.AddTransient<IUserService, UserService>();
       
        builder.Services.AddIdentityApiEndpoints<User>(options =>

        {

        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders(); // Essential for authentication

        using (var scope = builder.Services.BuildServiceProvider().CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var adminRoleExists = roleManager.RoleExistsAsync("Admin").Result;
            if (!adminRoleExists)
            {
                var adminRole = new IdentityRole("Admin");
                var result = roleManager.CreateAsync(adminRole).Result;
                if (!result.Succeeded)
                {
                }
            }

            var sellerRoleExists = roleManager.RoleExistsAsync("Seller").Result;
            if (!sellerRoleExists)
            {
                var sellerRole = new IdentityRole("Seller");
                var result = roleManager.CreateAsync(sellerRole).Result;
                if (!result.Succeeded)
                {
                }
            }
        }


        var app = builder.Build();

        // Configure the HTTP request pipeline.
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

