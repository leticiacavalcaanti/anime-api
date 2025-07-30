using AnimeApp.Application.DTOs;
using AnimeApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnimeApp.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AnimeController(IAnimeService animeService) : ControllerBase
{
    private readonly IAnimeService _animeService = animeService;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? nome, [FromQuery] string? diretor)
    {
        var result = await _animeService.GetByFilterAsync(nome, diretor);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var anime = await _animeService.GetByIdAsync(id);
        return anime is null ? NotFound() : Ok(anime);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AnimeRequest request)
    {
        var id = await _animeService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AnimeRequest request)
    {
        var success = await _animeService.UpdateAsync(id, request);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _animeService.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
