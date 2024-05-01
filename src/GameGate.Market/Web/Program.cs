using Application.Common.AssemblyReferences;
using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.DeleteExpiresOrdersService;
using Infrastructure.HttpClients;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Presentation.Controllers;
using System.Net.Http;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddApplicationPart(typeof(BuyGameController).Assembly); ;
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(assembly =>
        {
            assembly.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
        });
        builder.Services.AddTransient<IMarketRepository, MarketRepository>();
        builder.Services.AddDbContext<Database>((options) => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")), ServiceLifetime.Transient);
        builder.Services.AddHostedService<DeleteExpiresOrdersService>();
        builder.Services.AddHttpClient<IAuthHttpClient, AuthHttpClient>(httpClient => new AuthHttpClient(httpClient, builder.Configuration.GetValue<string>("AuthServerUrl")));
        builder.Services.AddHttpClient<IGamesHttpClient, GamesHttpClient>(httpClient => new GamesHttpClient(httpClient, builder.Configuration.GetValue<string>("GameServerUrl")));

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Market service");
            c.RoutePrefix = string.Empty;
        });

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}