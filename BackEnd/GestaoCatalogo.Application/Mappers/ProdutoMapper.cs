using GestaoCatalogo.Application.Dtos;
using GestaoCatalogo.Domain.Entities;

namespace GestaoCatalogo.Application.Mappers;

public static class ProdutoMapper
{
    public static ProdutoDto FromEntityToDto(this ProdutoEntity entity)
    {
        return new ProdutoDto
        {
            Id = entity.Id,
            Nome = entity.Nome,
            Descricao = entity.Descricao,
            Preco = entity.Preco,
            Categoria = new CategoriaDto
            {
                Id = entity.Categoria?.Id ?? entity.CategoriaId,
                Nome = entity.Categoria?.Nome,
                Descricao = entity.Categoria?.Descricao
            }
        };
    }

    public static ProdutoEntity FromDtoToEntity(this ProdutoDto? dto)
    {
        return new ProdutoEntity
        {
            Id = dto?.Id ?? 0,
            Nome = dto?.Nome ?? "",
            Descricao = dto?.Descricao ?? "",
            Preco = dto?.Preco ?? 0,
            CategoriaId = dto?.Categoria?.Id ?? 0
        };
    }

    public static IEnumerable<ProdutoEntity> FromEntitiesToDtos(this IEnumerable<ProdutoDto> dtos)
    {
        return dtos.Select(d => d.FromDtoToEntity());
    }

    public static IEnumerable<ProdutoDto> FromEntitiesToDtos(this IEnumerable<ProdutoEntity> entities)
    {
        return entities.Select(d => d.FromEntityToDto());
    }
}
