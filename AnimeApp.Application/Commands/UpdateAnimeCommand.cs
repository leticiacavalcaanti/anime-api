using AnimeApp.Application.DTOs;
using AnimeApp.Application.Interfaces;
using AnimeApp.Domain.Entities;
using AnimeApp.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace AnimeApp.Application.Commands;

public record UpdateAnimeCommand(Guid Id, string? Nome, string? Diretor, string? Resumo) : IRequest<AnimeResponse?>;

public class UpdateAnimeCommandHandler(IAnimeRepository repository, IMapper mapper) : IRequestHandler<UpdateAnimeCommand, AnimeResponse?>
{
    public async Task<AnimeResponse?> Handle(UpdateAnimeCommand request, CancellationToken cancellationToken)
    {
        var anime = await repository.GetByIdAsync(request.Id) ?? throw new AnimeNotFoundException(request.Id);

        anime.Nome = request.Nome ?? anime.Nome;
        anime.Diretor = request.Diretor ?? anime.Diretor;
        anime.Resumo = request.Resumo ?? anime.Resumo;

        await repository.UpdateAsync(anime);
        return mapper.Map<AnimeResponse>(anime);
    }
}
