using GestaoCatalogo.Application.Dtos;

namespace GestaoCatalogo.Application.Interfaces;

public interface IProdutoService
{
    Task<ApiResponseDto<ProdutoDto?>> GetByIdAsync(int id);
    Task<ApiResponseDto<IEnumerable<ProdutoDto>?>> GetAllAsync();
    Task<ApiResponseDto<ProdutoDto?>> AddAsync(ProdutoDto produto);
    Task<ApiResponseDto<object?>> DeleteAsync(int id);
    Task<ApiResponseDto<ProdutoDto?>> UpdateAsync(int id, ProdutoDto produto);
}
