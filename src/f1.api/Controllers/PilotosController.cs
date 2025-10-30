using AutoMapper;
using F1.Api.Application.DTOs;
using F1.Api.Domain.Entities;
using F1.Api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace f1.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PilotosController : ControllerBase
{
    private readonly IPilotoRepository _pilotoRepository;
    private readonly IEquipeRepository _equipeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PilotosController> _logger;

    public PilotosController(
        IPilotoRepository pilotoRepository,
        IEquipeRepository equipeRepository,
        IMapper mapper,
        ILogger<PilotosController> logger)
    {
        _pilotoRepository = pilotoRepository;
        _equipeRepository = equipeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PilotoDTO>>> GetAll()
    {
        var pilotos = await _pilotoRepository.GetAllAsync();
        var pilotosDto = _mapper.Map<IEnumerable<PilotoDTO>>(pilotos);
        return Ok(pilotosDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PilotoDTO>> GetById(int id)
    {
        var piloto = await _pilotoRepository.GetByIdAsync(id);
        
        if (piloto == null)
        {
            _logger.LogWarning("Piloto com ID {Id} não encontrado", id);
            return NotFound(new { message = $"Piloto com ID {id} não encontrado." });
        }

        var pilotoDto = _mapper.Map<PilotoDTO>(piloto);
        return Ok(pilotoDto);
    }

    [HttpGet("equipe/{equipeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<PilotoDTO>>> GetByEquipe(int equipeId)
    {
        if (!await _equipeRepository.ExistsAsync(equipeId))
        {
            return NotFound(new { message = $"Equipe com ID {equipeId} não encontrada." });
        }

        var pilotos = await _pilotoRepository.GetByEquipeIdAsync(equipeId);
        var pilotosDto = _mapper.Map<IEnumerable<PilotoDTO>>(pilotos);
        return Ok(pilotosDto);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PilotoDTO>> Create([FromBody] CreatePilotoDTO createDto)
    {

        if (!await _equipeRepository.ExistsAsync(createDto.EquipeId))
        {
            return BadRequest(new { message = $"Equipe com ID {createDto.EquipeId} não encontrada." });
        }

        try
        {
            var piloto = new Piloto(
                createDto.Nome, 
                createDto.Nacionalidade, 
                createDto.EquipeId, 
                createDto.DataNascimento);
            
            var createdPiloto = await _pilotoRepository.CreateAsync(piloto);
            var pilotoDto = _mapper.Map<PilotoDTO>(createdPiloto);

            _logger.LogInformation("Piloto {Nome} criado com sucesso", piloto.Nome);
            return CreatedAtAction(nameof(GetById), new { id = pilotoDto.PilotoId }, pilotoDto);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Erro de validação ao criar piloto");
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePilotoDTO updateDto)
    {
        var piloto = await _pilotoRepository.GetByIdAsync(id);
        
        if (piloto == null)
        {
            _logger.LogWarning("Tentativa de atualizar piloto inexistente ID {Id}", id);
            return NotFound(new { message = $"Piloto com ID {id} não encontrado." });
        }

        if (!await _equipeRepository.ExistsAsync(updateDto.EquipeId))
        {
            return BadRequest(new { message = $"Equipe com ID {updateDto.EquipeId} não encontrada." });
        }

        try
        {
            piloto.AtualizarNome(updateDto.Nome);
            piloto.AtualizarNacionalidade(updateDto.Nacionalidade);
            piloto.AtualizarEquipe(updateDto.EquipeId);
            piloto.AtualizarDataNascimento(updateDto.DataNascimento);

            await _pilotoRepository.UpdateAsync(piloto);
            
            _logger.LogInformation("Piloto ID {Id} atualizado com sucesso", id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Erro de validação ao atualizar piloto ID {Id}", id);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _pilotoRepository.DeleteAsync(id);
        
        if (!deleted)
        {
            _logger.LogWarning("Tentativa de deletar piloto inexistente ID {Id}", id);
            return NotFound(new { message = $"Piloto com ID {id} não encontrado." });
        }

        _logger.LogInformation("Piloto ID {Id} deletado com sucesso", id);
        return NoContent();
    }
}

