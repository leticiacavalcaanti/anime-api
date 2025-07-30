using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeApp.Application.DTOs;

namespace AnimeApp.Application.Interfaces;

public interface IAnimeService
{
    Task<IEnumerable<AnimeDTO>> GetAllAsync();
    Task<AnimeDTO?> GetByIdAsync(Guid id);
    Task<IEnumerable<AnimeDTO>> GetByFilterAsync(string? nome, string? diretor);
    Task<Guid> CreateAsync(AnimeRequest request);
    Task<bool> UpdateAsync(Guid id, AnimeRequest request);
    Task<bool> DeleteAsync(Guid id);
}