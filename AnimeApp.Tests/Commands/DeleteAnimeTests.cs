using AnimeApp.Application.Commands;
using AnimeApp.Application.DTOs;
using AnimeApp.Application.Interfaces;
using AnimeApp.Application.Queries;
using AnimeApp.Domain.Entities;
using AnimeApp.Domain.Exceptions;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AnimeApp.Tests.Commands;

public class DeleteAnimeCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldDeleteAnime_WhenFound()
    {
        var id = Guid.NewGuid();
        var anime = new Anime { Id = id };

        var repositoryMock = new Mock<IAnimeRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(anime);
        repositoryMock.Setup(r => r.DeleteAsync(anime)).Returns(Task.CompletedTask);

        var handler = new DeleteAnimeCommandHandler(repositoryMock.Object);
        var result = await handler.Handle(new DeleteAnimeCommand(id), default);

        result.Should().BeTrue();
        repositoryMock.Verify(r => r.DeleteAsync(anime), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenAnimeNotFound()
    {
        var id = Guid.NewGuid();
        var repositoryMock = new Mock<IAnimeRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Anime?)null);

        var handler = new DeleteAnimeCommandHandler(repositoryMock.Object);

        var act = async () => await handler.Handle(new DeleteAnimeCommand(id), default);

        await act.Should()
            .ThrowAsync<AnimeNotFoundException>()
            .WithMessage($"Anime com ID {id} não encontrado.");
    }

}