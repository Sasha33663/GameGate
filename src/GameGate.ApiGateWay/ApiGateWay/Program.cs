using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot();
var app = builder.Build();
await app.UseOcelot();
// Configure the HTTP request pipeline.
app.Run();
