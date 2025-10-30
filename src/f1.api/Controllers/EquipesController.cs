using AutoMapper;
using F1.Api.Application.DTOs;
using F1.Api.Domain.Entities;
using F1.Api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace f1.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EquipesController : ControllerBase
{
    private readonly IEquipeRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<EquipesController> _logger;

    public EquipesController(
        IEquipeRepository repository, 
        IMapper mapper,
        ILogger<EquipesController> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EquipeDTO>>> GetAll()
    {
        var equipes = await _repository.GetAllAsync();
        var equipesDto = _mapper.Map<IEnumerable<EquipeDTO>>(equipes);
        return Ok(equipesDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EquipeDTO>> GetById(int id)
    {
        var equipe = await _repository.GetByIdAsync(id);
        
        if (equipe == null)
        {
            _logger.LogWarning("Equipe com ID {Id} não encontrada", id);
            return NotFound(new { message = $"Equipe com ID {id} não encontrada." });
        }

        var equipeDto = _mapper.Map<EquipeDTO>(equipe);
        return Ok(equipeDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EquipeDTO>> Create([FromBody] CreateEquipeDTO createDto)
    {
        try
        {
            var equipe = new Equipe(createDto.Nome, createDto.Pais, createDto.AnoFundacao);
            var createdEquipe = await _repository.CreateAsync(equipe);
            var equipeDto = _mapper.Map<EquipeDTO>(createdEquipe);

            _logger.LogInformation("Equipe {Nome} criada com sucesso", equipe.Nome);
            return CreatedAtAction(nameof(GetById), new { id = equipeDto.EquipeId }, equipeDto);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Erro de validação ao criar equipe");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEquipeDTO updateDto)
    {
        var equipe = await _repository.GetByIdAsync(id);
        
        if (equipe == null)
        {
            _logger.LogWarning("Tentativa de atualizar equipe inexistente ID {Id}", id);
            return NotFound(new { message = $"Equipe com ID {id} não encontrada." });
        }

        try
        {
            equipe.AtualizarNome(updateDto.Nome);
            equipe.AtualizarPais(updateDto.Pais);
            equipe.AtualizarAnoFundacao(updateDto.AnoFundacao);

            await _repository.UpdateAsync(equipe);
            
            _logger.LogInformation("Equipe ID {Id} atualizada com sucesso", id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Erro de validação ao atualizar equipe ID {Id}", id);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        
        if (!deleted)
        {
            _logger.LogWarning("Tentativa de deletar equipe inexistente ID {Id}", id);
            return NotFound(new { message = $"Equipe com ID {id} não encontrada." });
        }

        _logger.LogInformation("Equipe ID {Id} deletada com sucesso", id);
        return NoContent();
    }
}

