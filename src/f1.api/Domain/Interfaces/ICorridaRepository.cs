using F1.Api.Domain.Entities;

namespace F1.Api.Domain.Interfaces;

public interface ICorridaRepository
{
    Task<IEnumerable<Corrida>> GetAllAsync();
    Task<Corrida?> GetByIdAsync(int id);
    Task<Corrida> AddAsync(Corrida corrida);
    Task<Corrida> UpdateAsync(Corrida corrida);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

