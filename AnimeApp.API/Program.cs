using AnimeApp.Infra.Context;
using AnimeApp.Infra.Repositories;
using AnimeApp.Application.Interfaces;
using AnimeApp.Application.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AnimeApp.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

/* AddDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
*/

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("Default"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default"))
    )
);

// AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

// Repositories (Infra)
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();

// Services (Application)
builder.Services.AddScoped<IAnimeService, AnimeService>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AnimeApp.API",
        Version = "v1"
    });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AnimeApp API v1");
        options.RoutePrefix = "swagger"; // ou "" para servir diretamente na raiz
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
