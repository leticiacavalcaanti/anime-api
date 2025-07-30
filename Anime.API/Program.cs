using AnimeApp.Infra.Context;
using AnimeApp.Infra.Repositories;
using AnimeApp.Application.Interfaces;
using AnimeApp.Application.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AnimeApp.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// AddDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
