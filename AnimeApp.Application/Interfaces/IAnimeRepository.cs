using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeApp.Domain.Entities;

namespace AnimeApp.Application.Interfaces;

public interface IAnimeRepository
{
    Task<IEnumerable<Anime>> GetAllAsync();
    Task<Anime?> GetByIdAsync(Guid id);
    Task<IEnumerable<Anime>> GetByFilterAsync(string? nome, string? diretor);
    Task AddAsync(Anime anime);
    Task UpdateAsync(Anime anime);
    Task DeleteAsync(Anime anime);
}


