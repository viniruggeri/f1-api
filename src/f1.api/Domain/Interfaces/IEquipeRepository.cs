using F1.Api.Domain.Entities;

namespace F1.Api.Domain.Interfaces;

public interface IEquipeRepository
{
    Task<IEnumerable<Equipe>> GetAllAsync();
    Task<Equipe?> GetByIdAsync(int id);
    Task<Equipe> CreateAsync(Equipe equipe);
    Task<Equipe> UpdateAsync(Equipe equipe);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}

