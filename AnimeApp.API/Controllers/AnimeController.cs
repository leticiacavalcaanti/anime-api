using AnimeApp.Application.Commands;
using AnimeApp.Application.DTOs;
using AnimeApp.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AnimeApp.API.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class AnimeController(IMediator mediator, ILogger<AnimeController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<AnimeController> _logger = logger;

        // Buscar todos ou filtrado
        [HttpGet]
        public async Task<IActionResult> GetFiltered([FromQuery] Guid? id, [FromQuery] string? nome, [FromQuery] string? diretor)
        {
            _logger.LogInformation("Iniciando busca de animes com filtros Id={Id}, Nome={Nome}, Diretor={Diretor}", id, nome, diretor);
            try
            {
                var result = await _mediator.Send(new GetAnimeByFilterQuery(id, nome, diretor));
                if (!result.Any())
                    return NotFound("Nenhum anime encontrado.");

                _logger.LogInformation("Retornando {Count} animes encontrados", result.Count());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao filtrar animes.");
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        // Buscar por ID diretamente
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Buscando anime por ID: {Id}", id);
            try
            {
                var result = await _mediator.Send(new GetAnimeByIdQuery(id));
                if (result == null)
                    return NotFound("Anime não encontrado.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar anime por ID: {Id}", id);
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        // Criar novo anime
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnimeRequest request)
        {
            _logger.LogInformation("Iniciando criação de anime.");
            try
            {
                var result = await _mediator.Send(new CreateAnimeCommand(request.Nome, request.Diretor, request.Resumo));
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar anime.");
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        // Atualizar anime
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAnimeRequest request)
        {
            _logger.LogInformation("Atualizando anime ID: {Id}", id);
            try
            {
                var result = await _mediator.Send(new UpdateAnimeCommand(id, request.Nome, request.Diretor, request.Resumo));
                if (result == null)
                    return NotFound("Anime não encontrado.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar anime ID: {Id}", id);
                return StatusCode(500, "Erro interno no servidor.");
            }
        }

        // Excluir anime
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Excluindo anime ID: {Id}", id);
            try
            {
                var result = await _mediator.Send(new DeleteAnimeCommand(id));
                if (!result)
                    return NotFound("Anime não encontrado.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir anime ID: {Id}", id);
                return StatusCode(500, "Erro interno no servidor.");
            }
        }
    }
}
