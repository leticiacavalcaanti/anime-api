using AnimeApp.Application.DTOs;
using AnimeApp.Application.Interfaces;
using AnimeApp.Domain.Entities;
using AutoMapper;

namespace AnimeApp.Application.Services;

public class AnimeService(IAnimeRepository repository, IMapper mapper) : IAnimeService
{
    private readonly IAnimeRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<AnimeDTO>> GetAllAsync()
    {
        var animes = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<AnimeDTO>>(animes);
    }

    public async Task<AnimeDTO?> GetByIdAsync(Guid id)
    {
        var anime = await _repository.GetByIdAsync(id);
        return anime is null ? null : _mapper.Map<AnimeDTO>(anime);
    }

    public async Task<IEnumerable<AnimeDTO>> GetByFilterAsync(string? nome, string? diretor)
    {
        var filtered = await _repository.GetByFilterAsync(nome, diretor);
        return _mapper.Map<IEnumerable<AnimeDTO>>(filtered);
    }

    public async Task<Guid> CreateAsync(AnimeRequest request)
    {
        var anime = _mapper.Map<Anime>(request);
        anime.Id = Guid.NewGuid();
        await _repository.AddAsync(anime);
        return anime.Id;
    }

    public async Task<bool> UpdateAsync(Guid id, AnimeRequest request)
    {
        var anime = await _repository.GetByIdAsync(id);
        if (anime is null) return false;

        anime.Nome = request.Nome;
        anime.Diretor = request.Diretor;
        anime.Resumo = request.Resumo;

        await _repository.UpdateAsync(anime);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var anime = await _repository.GetByIdAsync(id);
        if (anime is null) return false;

        await _repository.DeleteAsync(anime);
        return true;
    }
}
