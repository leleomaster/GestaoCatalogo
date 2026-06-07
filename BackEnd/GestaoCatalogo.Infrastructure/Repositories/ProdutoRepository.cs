using GestaoCatalogo.Domain.Entities;
using GestaoCatalogo.Domain.Interfaces;
using GestaoCatalogo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestaoCatalogo.Infrastructure.Repositories;

public class ProdutoRepository(AppDbContext context) : IProdutoRepository
{
    private readonly AppDbContext _context = context;

    public async Task<ProdutoEntity?> GetByIdAsync(int id)
    {
        return await _context.Produtos
                             .Include(p => p.Categoria)
                             .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<ProdutoEntity>?> GetAllAsync()
    {
        return await _context.Produtos
                             .Include(p => p.Categoria)
                             .ToListAsync();
    }

    public async Task<ProdutoEntity> AddAsync(ProdutoEntity produto)
    {
        await _context.Produtos.AddAsync(produto);
        await _context.SaveChangesAsync();

        return await _context.Produtos
                                .Include(p => p.Categoria)
                                .FirstOrDefaultAsync(p => p.Id == produto.Id) ?? produto;
    }

    public async Task DeleteAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto != null)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ProdutoEntity> UpdateAsync(ProdutoEntity produto)
    {
        _context.Produtos.Update(produto);

        await _context.SaveChangesAsync();

        return await _context.Produtos
                                 .Include(p => p.Categoria)
                                 .FirstOrDefaultAsync(p => p.Id == produto.Id) ?? produto;
    }
}
