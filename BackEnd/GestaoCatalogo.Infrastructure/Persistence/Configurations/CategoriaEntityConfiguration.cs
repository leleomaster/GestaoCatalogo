using GestaoCatalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoCatalogo.Infrastructure.Persistence.Configurations;

public class CategoriaEntityConfiguration : IEntityTypeConfiguration<CategoriaEntity>
{
    public void Configure(EntityTypeBuilder<CategoriaEntity> builder)
    {
        builder.ToTable("Categorias");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.Descricao)
               .HasMaxLength(250);

        builder.HasMany(c => c.Produtos)
               .WithOne(p => p.Categoria)
               .HasForeignKey(p => p.CategoriaId)
               .OnDelete(DeleteBehavior.Cascade);


    }
}
