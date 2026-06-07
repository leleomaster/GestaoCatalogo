using GestaoCatalogo.Application.Dtos;

namespace GestaoCatalogo.Application.Interfaces;

public interface ICategoriaService
{
    Task<ApiResponseDto<CategoriaDto?>> GetByIdAsync(int id);
    Task<ApiResponseDto<IEnumerable<CategoriaDto>?>> GetAllAsync();
    Task<ApiResponseDto<CategoriaDto?>> AddAsync(CategoriaDto categoria);
    Task<ApiResponseDto<object?>> DeleteAsync(int id);
    Task<ApiResponseDto<CategoriaDto?>> UpdateAsync(int id, CategoriaDto categoria);
}
