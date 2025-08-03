using AnimeApp.Application.Commands;
using AnimeApp.Application.DTOs;
using AnimeApp.Application.Interfaces;
using AnimeApp.Domain.Entities;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AnimeApp.Tests.Commands;

public class CreateAnimeCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateAnime_AndReturnDto()
    {
        // Arrange
        var command = new CreateAnimeCommand("Naruto", "Masashi Kishimoto", "Ninjas da folha");

        var expectedAnime = new Anime
        {
            Id = Guid.NewGuid(),
            Nome = command.Nome,
            Diretor = command.Diretor,
            Resumo = command.Resumo
        };

        var expectedDto = new AnimeResponse
        {
            Id = Guid.NewGuid(),
            Nome = "Naruto",
            Diretor = "Kishimoto",
            Resumo = "Ninjas"
        };


        var repositoryMock = new Mock<IAnimeRepository>();
        repositoryMock.Setup(r => r.AddAsync(It.IsAny<Anime>()))
              .ReturnsAsync((Anime anime) => anime);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<AnimeResponse>(It.IsAny<Anime>())).Returns(expectedDto);

        var handler = new CreateAnimeCommandHandler(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().BeEquivalentTo(expectedDto);
        repositoryMock.Verify(r => r.AddAsync(It.IsAny<Anime>()), Times.Once);
    }
}
