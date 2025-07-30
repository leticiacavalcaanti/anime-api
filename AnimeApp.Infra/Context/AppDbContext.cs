using AnimeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimeApp.Infra.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Anime> Animes { get; set; } = null!;
}