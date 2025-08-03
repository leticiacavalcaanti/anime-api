using AnimeApp.Application.Commands;
using AnimeApp.Application.DTOs;
using AnimeApp.Application.Interfaces;
using AnimeApp.Domain.Entities;
using AnimeApp.Domain.Exceptions;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace AnimeApp.Tests.Commands;

public class UpdateAnimeCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldUpdateAnime_WhenFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new UpdateAnimeCommand(id, "Atualizado", "Diretor X", "Resumo atualizado");

        var animeFromDb = new Anime
        {
            Id = id,
            Nome = "Original",
            Diretor = "Original",
            Resumo = "Original"
        };

        var updatedDto = new AnimeResponse
        {
            Id = id,
            Nome = command.Nome ?? animeFromDb.Nome,
            Diretor = command.Diretor ?? animeFromDb.Diretor,
            Resumo = command.Resumo ?? animeFromDb.Resumo
        };

        var repositoryMock = new Mock<IAnimeRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(animeFromDb);
        repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Anime>())).ReturnsAsync(animeFromDb);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<AnimeResponse>(It.IsAny<Anime>())).Returns(updatedDto);

        var handler = new UpdateAnimeCommandHandler(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().BeEquivalentTo(updatedDto);
        animeFromDb.Nome.Should().Be(command.Nome);
        repositoryMock.Verify(r => r.UpdateAsync(animeFromDb), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenAnimeNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new UpdateAnimeCommand(id, "Novo Nome", "Novo Diretor", "Novo Resumo");

        var repositoryMock = new Mock<IAnimeRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Anime?)null);

        var mapperMock = new Mock<IMapper>(); // <-- mock do AutoMapper

        var handler = new UpdateAnimeCommandHandler(repositoryMock.Object, mapperMock.Object);

        // Act + Assert
        await Assert.ThrowsAsync<AnimeNotFoundException>(() => handler.Handle(request, default));
    }

    [Fact]
    public async Task Handle_ShouldUpdateOnlyResumo_WhenOnlyResumoProvided()
    {
        // Arrange
        var id = Guid.NewGuid();
        var existingAnime = new Anime
        {
            Id = id,
            Nome = "Nome Original",
            Diretor = "Diretor Original",
            Resumo = "Resumo Original"
        };

        var novoResumo = "Resumo atualizado somente.";

        var command = new UpdateAnimeCommand(id, null, null, novoResumo);

        var repositoryMock = new Mock<IAnimeRepository>();
        repositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existingAnime);
        repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Anime>())).ReturnsAsync(existingAnime);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<AnimeResponse>(It.IsAny<Anime>())).Returns(
            (Anime anime) => new AnimeResponse
            {
                Id = anime.Id,
                Nome = anime.Nome,
                Diretor = anime.Diretor,
                Resumo = anime.Resumo
            });

        var handler = new UpdateAnimeCommandHandler(repositoryMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(id);
        result.Nome.Should().Be(existingAnime.Nome);
        result.Diretor.Should().Be(existingAnime.Diretor);
        result.Resumo.Should().Be(novoResumo);
    }



}
