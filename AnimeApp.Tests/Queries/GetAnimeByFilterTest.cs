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

namespace AnimeApp.Tests.Queries;

public class GetAnimeByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnDto_WhenAnimeExists()
    {
        var id = Guid.NewGuid();
        var anime = new Anime { Id = id, Nome = "Bleach", Diretor = "Kubo", Resumo = "Shinigamis" };
        var expectedDto = new AnimeResponse { Id = id, Nome = anime.Nome, Diretor = anime.Diretor, Resumo = anime.Resumo };

        var repositoryMock = new Mock<IAnimeRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(anime);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<AnimeResponse>(anime)).Returns(expectedDto);

        var handler = new GetAnimeByIdQueryHandler(repositoryMock.Object, mapperMock.Object);
        var result = await handler.Handle(new GetAnimeByIdQuery(id), default);

        result.Should().BeEquivalentTo(expectedDto);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenAnimeDoesNotExist()
    {
        var id = Guid.NewGuid();

        var repositoryMock = new Mock<IAnimeRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Anime?)null);

        var mapperMock = new Mock<IMapper>();
        var handler = new GetAnimeByIdQueryHandler(repositoryMock.Object, mapperMock.Object);

        await Assert.ThrowsAsync<AnimeNotFoundException>(() => handler.Handle(new GetAnimeByIdQuery(id), default));
    }

}
