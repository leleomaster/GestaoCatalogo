using GestaoCatalogo.Domain.Entities;

namespace GestaoCatalogo.Domain.Interfaces;

public interface ICategoriaRepository
{
    Task<CategoriaEntity?> GetByIdAsync(int id);
    Task<IEnumerable<CategoriaEntity>?> GetAllAsync();
    Task AddAsync(CategoriaEntity categoria);
    Task DeleteAsync(int id);
    Task<CategoriaEntity?> UpdateAsync(CategoriaEntity categoria);
}
