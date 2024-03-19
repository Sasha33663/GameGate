
using Application.Commands.Create;
using Application.Common.AssemblyReferences;
using Application.Common.Inteefaces;
using Infrastructure;
using Infrastructure.GameRepository;
using Infrastructure.GameRepository.HttpClients;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Presentation.Controllers;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        builder.Services.AddControllers().AddApplicationPart(typeof(GameController).Assembly); ;
        builder.Services.AddMediatR(assembly =>
        {
            assembly.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
        });
        builder.Services.AddDbContext<Database>((options) => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));
       builder.Services.AddTransient<IAuthorizationHttpClient , AuthorizationHttpClient>();
        builder.Services.AddTransient<IGameRepository, GameRepository>();
        builder.Services.AddHttpClient<IAuthorizationHttpClient, AuthorizationHttpClient>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
