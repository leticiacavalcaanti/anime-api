using AnimeApp.Application.DTOs;
using AnimeApp.Application.Interfaces;
using AnimeApp.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace AnimeApp.Application.Queries;

public record GetAnimeByIdQuery(Guid Id) : IRequest<AnimeDTO?>;

public class GetAnimeByIdQueryHandler(IAnimeRepository repository, IMapper mapper) : IRequestHandler<GetAnimeByIdQuery, AnimeDTO?>
{
    public async Task<AnimeDTO?> Handle(GetAnimeByIdQuery request, CancellationToken cancellationToken)
    {
        var anime = await repository.GetByIdAsync(request.Id);
        return anime is null ? throw new AnimeNotFoundException(request.Id) : mapper.Map<AnimeDTO>(anime);
    }
}