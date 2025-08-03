using AnimeApp.Application.Commands;
using AnimeApp.Application.DTOs;
using AnimeApp.Application.Interfaces;
using AnimeApp.Application.Queries;
using AnimeApp.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AnimeApp.Tests.Queries;

public class GetAnimeByFilterQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnFilteredAnimes()
    {
        var filtered = new List<Anime>
        {
            new() { Id = Guid.NewGuid(), Nome = "Naruto", Diretor = "Kishimoto", Resumo = "Ninja" },
            new() { Id = Guid.NewGuid(), Nome = "Boruto", Diretor = "Kishimoto", Resumo = "Ninja 2" }
        };

        var expected = filtered.Select(a => new AnimeResponse { Id = a.Id, Nome = a.Nome, Diretor = a.Diretor, Resumo = a.Resumo });

        var repositoryMock = new Mock<IAnimeRepository>();
        repositoryMock.Setup(r => r.GetByFilterAsync(null, null, null)).ReturnsAsync(filtered);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<IEnumerable<AnimeResponse>>(filtered)).Returns(expected);

        var handler = new GetAnimeByFilterQueryHandler(repositoryMock.Object, mapperMock.Object);
        var result = await handler.Handle(new GetAnimeByFilterQuery(null, null, null), default);

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoMatch()
    {
        var repositoryMock = new Mock<IAnimeRepository>();
        repositoryMock.Setup(r => r.GetByFilterAsync(null, null, null)).ReturnsAsync([]);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<IEnumerable<AnimeResponse>>(It.IsAny<IEnumerable<Anime>>())).Returns([]);

        var handler = new GetAnimeByFilterQueryHandler(repositoryMock.Object, mapperMock.Object);
        var result = await handler.Handle(new GetAnimeByFilterQuery(null, null, null), default);

        result.Should().BeEmpty();
    }
}