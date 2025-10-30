using F1.Api.Domain.Entities;
using F1.Api.Domain.Interfaces;
using F1.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace F1.Api.Infrastructure.Repositories;

public class EquipeRepository : IEquipeRepository
{
    private readonly F1DbContext _context;

    public EquipeRepository(F1DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Equipe>> GetAllAsync()
    {
        return await _context.Equipes
            .Include(e => e.Pilotos)
            .ToListAsync();
    }

    public async Task<Equipe?> GetByIdAsync(int id)
    {
        return await _context.Equipes
            .Include(e => e.Pilotos)
            .FirstOrDefaultAsync(e => e.EquipeId == id);
    }

    public async Task<Equipe> CreateAsync(Equipe equipe)
    {
        _context.Equipes.Add(equipe);
        await _context.SaveChangesAsync();
        return equipe;
    }

    public async Task<Equipe> UpdateAsync(Equipe equipe)
    {
        _context.Entry(equipe).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return equipe;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var equipe = await _context.Equipes.FindAsync(id);
        if (equipe == null)
            return false;

        _context.Equipes.Remove(equipe);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Equipes.AnyAsync(e => e.EquipeId == id);
    }
}

