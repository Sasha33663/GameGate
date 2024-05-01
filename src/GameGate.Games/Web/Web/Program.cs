using Application.Common.AssemblyReferences;
using Application.Common.Intefaces;
using Infrastructure;
using Infrastructure.GameRepository;
using Infrastructure.HttpClients;
using Infrastructure.ImageRepository;
using Microsoft.EntityFrameworkCore;
using Presentation.Controllers;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddApplicationPart(typeof(GameController).Assembly); ;
        builder.Services.AddMediatR(assembly =>
        {
            assembly.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
        });
        builder.Services.AddDbContext<Database>((options) => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")), ServiceLifetime.Transient);
        builder.Services.AddTransient<IGameRepository, GameRepository>();

        builder.Services.AddHttpClient<IAuthorizationHttpClient, AuthorizationHttpClient>(httpClient => new AuthorizationHttpClient(httpClient, builder.Configuration.GetValue<string>("AuthServerUrl")));
        builder.Services.AddTransient<IImageRepository, ImageRepository>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();


        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Games service");
            c.RoutePrefix = string.Empty;
        });


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}