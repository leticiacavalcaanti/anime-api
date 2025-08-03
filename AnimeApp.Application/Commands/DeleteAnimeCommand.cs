using AnimeApp.Application.Interfaces;
using AnimeApp.Domain.Exceptions;
using MediatR;

namespace AnimeApp.Application.Commands;

public record DeleteAnimeCommand(Guid Id) : IRequest<bool>;

public class DeleteAnimeCommandHandler(IAnimeRepository repository) : IRequestHandler<DeleteAnimeCommand, bool>
{
    public async Task<bool> Handle(DeleteAnimeCommand request, CancellationToken cancellationToken)
    {
        var anime = await repository.GetByIdAsync(request.Id)
        ?? throw new AnimeNotFoundException(request.Id);

        await repository.DeleteAsync(anime);
        return true;
    }
}