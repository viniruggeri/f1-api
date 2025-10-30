using F1.Api.Domain.Entities;
using F1.Api.Domain.Interfaces;
using F1.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace F1.Api.Infrastructure.Repositories;

public class CorridaRepository : ICorridaRepository
{
    private readonly F1DbContext _context;

    public CorridaRepository(F1DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Corrida>> GetAllAsync()
    {
        return await _context.Corridas
            .Include(c => c.Resultados)
            .ToListAsync();
    }

    public async Task<Corrida?> GetByIdAsync(int id)
    {
        return await _context.Corridas
            .Include(c => c.Resultados)
            .FirstOrDefaultAsync(c => c.CorridaId == id);
    }

    public async Task<Corrida> AddAsync(Corrida corrida)
    {
        await _context.Corridas.AddAsync(corrida);
        await _context.SaveChangesAsync();
        return corrida;
    }

    public async Task<Corrida> UpdateAsync(Corrida corrida)
    {
        _context.Corridas.Update(corrida);
        await _context.SaveChangesAsync();
        return corrida;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var corrida = await GetByIdAsync(id);
        if (corrida == null)
            return false;

        _context.Corridas.Remove(corrida);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Corridas.AnyAsync(c => c.CorridaId == id);
    }
}

