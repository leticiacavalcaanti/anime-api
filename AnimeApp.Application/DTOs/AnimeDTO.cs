using System.ComponentModel.DataAnnotations;

namespace AnimeApp.Application.DTOs;

public class AnimeResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Diretor { get; set; } = string.Empty;
    public string Resumo { get; set; } = string.Empty;
}

public class CreateAnimeRequest
{
    [Required]
    public string Nome { get; set; } = string.Empty;
    [Required]
    public string Diretor { get; set; } = string.Empty;
    [Required]
    public string Resumo { get; set; } = string.Empty;
}

public class UpdateAnimeRequest
{
    public string? Nome { get; set; }
    public string? Diretor { get; set; }
    public string? Resumo { get; set; }
}

