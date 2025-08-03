using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using AnimeApp.Infra.Context;

namespace AnimeApp.Infra.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer("Server=localhost;Database=AnimeDb;User Id=sa;Password=Your_password123;TrustServerCertificate=true");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
