using Application.Common.AssemblyReferences;
using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.DeleteExpiresOrdersService;
using Infrastructure.HttpClients;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Presentation.Controllers;

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
        builder.Services.AddHttpClient<IGamesHttpClient, GamestHttpClient>();
        builder.Services.AddTransient<IGamesHttpClient, GamestHttpClient>();
        builder.Services.AddHttpClient<IAuthHttpClient, AuthHttpClient>();
        builder.Services.AddTransient<IAuthHttpClient, AuthHttpClient>();
        builder.Services.AddTransient<IMarketRepository, MarketRepository>();
        builder.Services.AddDbContext<Database>((options) => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")), ServiceLifetime.Transient);
        builder.Services.AddHostedService<DeleteExpiresOrdersService>();

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