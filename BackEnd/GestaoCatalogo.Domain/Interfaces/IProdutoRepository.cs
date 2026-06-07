using GestaoCatalogo.Domain.Entities;

namespace GestaoCatalogo.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<ProdutoEntity?> GetByIdAsync(int id);
    Task<IEnumerable<ProdutoEntity>?> GetAllAsync();
    Task<ProdutoEntity> AddAsync(ProdutoEntity produto);
    Task DeleteAsync(int id);
    Task<ProdutoEntity> UpdateAsync(ProdutoEntity produto);
}
