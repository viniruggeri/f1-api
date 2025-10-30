using F1.Api.Domain.Entities;

namespace F1.Api.Domain.Interfaces;

public interface IPilotoRepository
{
    Task<IEnumerable<Piloto>> GetAllAsync();
    Task<Piloto?> GetByIdAsync(int id);
    Task<Piloto> CreateAsync(Piloto piloto);
    Task<Piloto> UpdateAsync(Piloto piloto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<Piloto>> GetByEquipeIdAsync(int equipeId);
}

