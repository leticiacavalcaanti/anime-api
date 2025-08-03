using AnimeApp.Application.DTOs;
using AnimeApp.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AnimeApp.Application.Queries;

public record GetAnimeByFilterQuery(Guid? Id, string? Nome, string? Diretor) : IRequest<IEnumerable<AnimeResponse>>;

public class GetAnimeByFilterQueryHandler(IAnimeRepository repository, IMapper mapper) : IRequestHandler<GetAnimeByFilterQuery, IEnumerable<AnimeResponse>>
{
    public async Task<IEnumerable<AnimeResponse>> Handle(GetAnimeByFilterQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetByFilterAsync(request.Id, request.Nome, request.Diretor);
        return mapper.Map<IEnumerable<AnimeResponse>>(result);
    }
}