using GestaoCatalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoCatalogo.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CategoriaEntity> Categorias { get; set; }
    public DbSet<ProdutoEntity> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
