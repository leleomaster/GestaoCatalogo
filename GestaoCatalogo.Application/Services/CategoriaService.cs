using GestaoCatalogo.Application.Cosnt;
using GestaoCatalogo.Application.Dtos;
using GestaoCatalogo.Application.Interfaces;
using GestaoCatalogo.Application.Mappers;
using GestaoCatalogo.Domain.Entities;
using GestaoCatalogo.Domain.Interfaces;

namespace GestaoCatalogo.Application.Services;

public class CategoriaService(ICategoriaRepository categoriaRepository) : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

    public async Task<ApiResponseDto<object?>> AddAsync(CategoriaDto categoria)
    {
        await _categoriaRepository.AddAsync(new CategoriaEntity
        {
            Nome = categoria.Nome,
            Descricao = categoria.Descricao
        });

        return ApiResponseDto<object?>.Ok(null);
    }

    public async Task<ApiResponseDto<object?>> DeleteAsync(int id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);

        if (categoria?.Produtos?.Any() == true)
        {
            return ApiResponseDto<object?>.Fail(MessagemErro.ProdutoAssociadoCategoria);
        }

        await _categoriaRepository.DeleteAsync(id);

        return ApiResponseDto<object?>.Ok(null);
    }

    public async Task<ApiResponseDto<IEnumerable<CategoriaDto>?>> GetAllAsync()
    {
        var categorias = await _categoriaRepository.GetAllAsync();

        if (categorias == null)
        {
            return ApiResponseDto<IEnumerable<CategoriaDto>?>.Fail(MessagemErro.CategoriaNaoEncontrada);
        }

        return ApiResponseDto<IEnumerable<CategoriaDto>?>.Ok(categorias.FromEntitiesToDtos());
    }

    public async Task<ApiResponseDto<CategoriaDto?>> GetByIdAsync(int id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);

        if (categoria == null)
        {
            return ApiResponseDto<CategoriaDto?>.Fail(MessagemErro.CategoriaNaoEncontrada);
        }

        return ApiResponseDto<CategoriaDto>.Ok(categoria.FromEntityToDto());
    }

    public async Task<ApiResponseDto<CategoriaDto?>> UpdateAsync(CategoriaDto categoria)
    {
        var categoriaEntity = await _categoriaRepository.UpdateAsync(categoria.FromDtoToEntity());

        return ApiResponseDto<CategoriaDto?>.Updated(categoriaEntity?.FromEntityToDto());
    }
}
