using AnimeApp.Domain.Entities;
using AnimeApp.Infra.Context;
using Microsoft.EntityFrameworkCore;
using AnimeApp.Application.Interfaces;

namespace AnimeApp.Infra.Repositories;

public class AnimeRepository(AppDbContext context) : IAnimeRepository // 👈 Construtor primário
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<Anime>> GetAllAsync()
    {
        return await _context.Animes.ToListAsync();
    }

    public async Task<Anime?> GetByIdAsync(Guid id)
    {
        return await _context.Animes.FindAsync(id);
    }

    public async Task<IEnumerable<Anime>> GetByFilterAsync(string? nome, string? diretor)
    {
        var query = _context.Animes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(a => a.Nome.Contains(nome));

        if (!string.IsNullOrWhiteSpace(diretor))
            query = query.Where(a => a.Diretor.Contains(diretor));

        return await query.ToListAsync();
    }

    public async Task AddAsync(Anime anime)
    {
        _context.Animes.Add(anime);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Anime anime)
    {
        _context.Animes.Update(anime);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Anime anime)
    {
        _context.Animes.Remove(anime);
        await _context.SaveChangesAsync();
    }
}
