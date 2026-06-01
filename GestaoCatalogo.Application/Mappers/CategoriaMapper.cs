using GestaoCatalogo.Application.Dtos;
using GestaoCatalogo.Domain.Entities;

namespace GestaoCatalogo.Application.Mappers;

public static class CategoriaMapper
{
    public static CategoriaDto FromEntityToDto(this CategoriaEntity entity)
    {
        return new CategoriaDto
        {
            Id = entity.Id,
            Nome = entity.Nome,
            Descricao = entity.Descricao,
            Produtos = entity.Produtos?.Select(p =>
            new ProdutoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.Preco,
            }).ToList()
        };
    }

    public static CategoriaEntity FromDtoToEntity(this CategoriaDto dto)
    {
        return new CategoriaEntity
        {
            Id = dto.Id,
            Nome = dto.Nome ?? "",
            Descricao = dto.Descricao ?? "",
            Produtos = dto.Produtos?.Select(p =>
            new ProdutoEntity
            {
                Id = p.Id,
                Nome = p?.Nome ?? "",
                Descricao = p?.Descricao ?? "",
                Preco = p?.Preco ?? 0,
            }).ToList()
        };
    }

    public static IEnumerable<CategoriaEntity> FromDtosToEntities(this IEnumerable<CategoriaDto> dtos)
    {
        return [.. dtos.Select(d => d.FromDtoToEntity())];
    }

    public static IEnumerable<CategoriaDto> FromEntitiesToDtos(this IEnumerable<CategoriaEntity> entities)
    {
        return [.. entities.Select(d => d.FromEntityToDto())];
    }
}
