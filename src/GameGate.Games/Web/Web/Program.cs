
using Application.Common.Inteefaces;
using Infrastructure;
using Infrastructure.GameRepository;
using Microsoft.EntityFrameworkCore;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddDbContext<Database>((options) => options.UseNpgsql("Server=localhost;Port=5432;Database=GameGate.Games; UserId=postgres;Password=Batonbatonbaton123;"));
        builder.Services.AddTransient<IGameRepository, GameRepository>();

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
