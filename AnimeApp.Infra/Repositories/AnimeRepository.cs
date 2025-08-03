using AnimeApp.Domain.Entities;
using AnimeApp.Infra.Context;
using Microsoft.EntityFrameworkCore;
using AnimeApp.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace AnimeApp.Infra.Repositories;

public class AnimeRepository(AppDbContext context, ILogger<AnimeRepository> logger) : IAnimeRepository
{
    private readonly AppDbContext _context = context;
    private readonly ILogger<AnimeRepository> _logger = logger;

    public async Task<IEnumerable<Anime>> GetAllAsync()
    {
        try
        {
            _logger.LogDebug("Buscando todos os animes...");
            return await _context.Animes.AsNoTracking().ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter todos os animes.");
            throw;
        }
    }

    public async Task<Anime?> GetByIdAsync(Guid id)
    {
        try
        {
            _logger.LogDebug("Buscando anime por ID: {Id}", id);
            return await _context.Animes.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter o anime por ID.");
            throw;
        }
    }

    public async Task<IEnumerable<Anime>> GetByFilterAsync(Guid? id, string? nome, string? diretor)
    {
        try
        {
            _logger.LogDebug("Filtrando animes com id={Id}, nome={Nome}, diretor={Diretor}", id, nome, diretor);

            var query = _context.Animes.AsNoTracking().AsQueryable();

            if (id.HasValue)
                query = query.Where(a => a.Id == id.Value);

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(a => a.Nome.Contains(nome));

            if (!string.IsNullOrWhiteSpace(diretor))
                query = query.Where(a => a.Diretor.Contains(diretor));

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao filtrar os animes.");
            throw;
        }
    }

    public async Task<Anime> AddAsync(Anime anime)
    {
        try
        {
            await _context.Animes.AddAsync(anime);
            await _context.SaveChangesAsync();
            return anime;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao adicionar anime.");
            throw;
        }
    }

    public async Task<Anime> UpdateAsync(Anime anime)
    {
        try
        {
            _context.Entry(anime).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return anime;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar anime.");
            throw;
        }
    }

    public async Task DeleteAsync(Anime anime)
    {
        try
        {
            _context.Animes.Remove(anime);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir anime.");
            throw;
        }
    }
}

