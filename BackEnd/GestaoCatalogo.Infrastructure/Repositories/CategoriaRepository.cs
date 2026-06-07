using GestaoCatalogo.Domain.Entities;
using GestaoCatalogo.Domain.Interfaces;
using GestaoCatalogo.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestaoCatalogo.Infrastructure.Repositories;

public class CategoriaRepository(AppDbContext context) : ICategoriaRepository
{
    private readonly AppDbContext _context = context;

    public async Task<CategoriaEntity?> GetByIdAsync(int id)
    {
        return await _context.Categorias
                             .Include(c => c.Produtos)
                             .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<CategoriaEntity>?> GetAllAsync()
    {
        return await _context.Categorias
                             .Include(c => c.Produtos)
                             .ToListAsync();
    }

    public async Task<CategoriaEntity> AddAsync(CategoriaEntity categoria)
    {
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();

        return categoria;
    }

    public async Task DeleteAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria != null)
        {
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<CategoriaEntity> UpdateAsync(CategoriaEntity categoria)
    {
        _context.Categorias.Update(categoria);

        await _context.SaveChangesAsync();
        return categoria;
    }
}
