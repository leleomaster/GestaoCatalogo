using GestaoCatalogo.Application.Cosnt;
using GestaoCatalogo.Application.Dtos;
using GestaoCatalogo.Application.Interfaces;
using GestaoCatalogo.Application.Mappers;
using GestaoCatalogo.Domain.Interfaces;

namespace GestaoCatalogo.Application.Services;

public class CategoriaService(ICategoriaRepository categoriaRepository) : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

    public async Task<ApiResponseDto<CategoriaDto?>> AddAsync(CategoriaDto categoria)
    {
        var categoriaEntity = await _categoriaRepository.AddAsync(categoria.FromDtoToEntity());

        return ApiResponseDto<CategoriaDto?>.Created(categoriaEntity?.FromEntityToDto());
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

    public async Task<ApiResponseDto<CategoriaDto?>> UpdateAsync(int id, CategoriaDto dto)
    {
        var categoriaEntity = await _categoriaRepository.GetByIdAsync(id);
        if (categoriaEntity == null)
            return ApiResponseDto<CategoriaDto>.Fail(MessagemErro.CategoriaNaoEncontrada);
        else
        {
            categoriaEntity.Descricao = dto.Descricao?? categoriaEntity.Descricao;
            categoriaEntity.Nome = dto.Nome ?? categoriaEntity.Nome;
            
            var resultado = await _categoriaRepository.UpdateAsync(categoriaEntity);

            return ApiResponseDto<CategoriaDto?>.Updated(resultado?.FromEntityToDto());
        }
    }
}
