using AnimeApp.Application.DTOs;
using AnimeApp.Application.Interfaces;
using AnimeApp.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AnimeApp.Application.Commands;

public record CreateAnimeCommand(string Nome, string Diretor, string Resumo) : IRequest<AnimeResponse>;

public class CreateAnimeCommandHandler(IAnimeRepository repository, IMapper mapper) : IRequestHandler<CreateAnimeCommand, AnimeResponse>
{
    public async Task<AnimeResponse> Handle(CreateAnimeCommand request, CancellationToken cancellationToken)
    {
        var anime = new Anime
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Diretor = request.Diretor,
            Resumo = request.Resumo
        };

        await repository.AddAsync(anime);
        return mapper.Map<AnimeResponse>(anime);
    }
}