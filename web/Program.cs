
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Infrastructure;
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
        builder.Services.AddDbContext<DatabaseContext>((options) => options.UseNpgsql("Server=localhost;Port=5432;Database=GameGate.Authorization; UserId=postgres;Password=Batonbatonbaton123;"));
        builder.Services.AddDefaultIdentity<User>(options => 
               {
           
        }).AddEntityFrameworkStores<DatabaseContext>();

        //builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        //    .AddEntityFrameworkStores<DatabaseContext>()
        //    .AddDefaultTokenProviders();



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.MapIdentityApi<User>();

        app.UseHttpsRedirection();

        
        
       
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
