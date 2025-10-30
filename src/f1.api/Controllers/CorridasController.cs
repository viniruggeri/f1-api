using AutoMapper;
using F1.Api.Domain.Entities;
using F1.Api.Domain.Interfaces;
using F1.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace F1.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CorridasController : ControllerBase
{
    private readonly ICorridaRepository _corridaRepository;
    private readonly IMapper _mapper;

    public CorridasController(ICorridaRepository corridaRepository, IMapper mapper)
    {
        _corridaRepository = corridaRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CorridaDTO>>> GetAll()
    {
        var corridas = await _corridaRepository.GetAllAsync();
        var corridasDto = _mapper.Map<IEnumerable<CorridaDTO>>(corridas);
        return Ok(corridasDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CorridaDTO>> GetById(int id)
    {
        var corrida = await _corridaRepository.GetByIdAsync(id);
        
        if (corrida == null)
            return NotFound(new { message = $"Corrida com ID {id} não encontrada." });

        var corridaDto = _mapper.Map<CorridaDTO>(corrida);
        return Ok(corridaDto);
    }

    [HttpPost]
    public async Task<ActionResult<CorridaDTO>> Create([FromBody] CorridaDTO corridaDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var corrida = _mapper.Map<Corrida>(corridaDto);
            var corridaCriada = await _corridaRepository.AddAsync(corrida);
            var corridaCriadaDto = _mapper.Map<CorridaDTO>(corridaCriada);

            return CreatedAtAction(
                nameof(GetById),
                new { id = corridaCriadaDto.CorridaId },
                corridaCriadaDto
            );
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CorridaDTO>> Update(int id, [FromBody] CorridaDTO corridaDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var corridaExistente = await _corridaRepository.GetByIdAsync(id);
        if (corridaExistente == null)
            return NotFound(new { message = $"Corrida com ID {id} não encontrada." });

        try
        {
            corridaExistente.Nome = corridaDto.Nome;
            corridaExistente.Local = corridaDto.Local;
            corridaExistente.Data = corridaDto.Data;

            var corridaAtualizada = await _corridaRepository.UpdateAsync(corridaExistente);
            var corridaAtualizadaDto = _mapper.Map<CorridaDTO>(corridaAtualizada);

            return Ok(corridaAtualizadaDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var corridaExistente = await _corridaRepository.GetByIdAsync(id);
        if (corridaExistente == null)
            return NotFound(new { message = $"Corrida com ID {id} não encontrada." });

        var deletado = await _corridaRepository.DeleteAsync(id);
        
        if (!deletado)
            return BadRequest(new { message = "Erro ao deletar corrida." });

        return NoContent();
    }
}

