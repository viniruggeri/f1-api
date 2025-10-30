using F1.Api.Domain.Entities;
using F1.Api.Domain.Interfaces;
using F1.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace F1.Api.Infrastructure.Repositories;

public class PilotoRepository : IPilotoRepository
{
    private readonly F1DbContext _context;

    public PilotoRepository(F1DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Piloto>> GetAllAsync()
    {
        return await _context.Pilotos
            .Include(p => p.Equipe)
            .ToListAsync();
    }

    public async Task<Piloto?> GetByIdAsync(int id)
    {
        return await _context.Pilotos
            .Include(p => p.Equipe)
            .FirstOrDefaultAsync(p => p.PilotoId == id);
    }

    public async Task<Piloto> CreateAsync(Piloto piloto)
    {
        _context.Pilotos.Add(piloto);
        await _context.SaveChangesAsync();
        return await GetByIdAsync(piloto.PilotoId) ?? piloto;
    }

    public async Task<Piloto> UpdateAsync(Piloto piloto)
    {
        _context.Entry(piloto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return await GetByIdAsync(piloto.PilotoId) ?? piloto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var piloto = await _context.Pilotos.FindAsync(id);
        if (piloto == null)
            return false;

        _context.Pilotos.Remove(piloto);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Pilotos.AnyAsync(p => p.PilotoId == id);
    }

    public async Task<IEnumerable<Piloto>> GetByEquipeIdAsync(int equipeId)
    {
        return await _context.Pilotos
            .Include(p => p.Equipe)
            .Where(p => p.EquipeId == equipeId)
            .ToListAsync();
    }
}

