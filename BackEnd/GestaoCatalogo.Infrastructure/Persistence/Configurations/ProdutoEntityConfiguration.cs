using GestaoCatalogo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoCatalogo.Infrastructure.Persistence.Configurations;

public class ProdutoEntityConfiguration : IEntityTypeConfiguration<ProdutoEntity>
{
    public void Configure(EntityTypeBuilder<ProdutoEntity> builder)
    {
        builder.ToTable("Produtos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Descricao)
               .HasMaxLength(250);

        builder.Property(p => p.Preco)
               .HasColumnType("decimal(18,2)");
    }
}
