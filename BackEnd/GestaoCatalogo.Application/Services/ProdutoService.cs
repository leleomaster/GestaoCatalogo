using GestaoCatalogo.Application.Cosnt;
using GestaoCatalogo.Application.Dtos;
using GestaoCatalogo.Application.Interfaces;
using GestaoCatalogo.Application.Mappers;
using GestaoCatalogo.Domain.Interfaces;

namespace GestaoCatalogo.Application.Services;

public class ProdutoService(IProdutoRepository produtoRepository) : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository = produtoRepository;

    public async Task<ApiResponseDto<ProdutoDto?>> AddAsync(ProdutoDto produto)
    {
        var produtoEntity = await _produtoRepository.AddAsync(produto.FromDtoToEntity());

        return ApiResponseDto<ProdutoDto?>.Created(produtoEntity.FromEntityToDto());
    }

    public async Task<ApiResponseDto<object?>> DeleteAsync(int id)
    {  
        await _produtoRepository.DeleteAsync(id);

        return  ApiResponseDto<object?>.Deleted();
    }

    public async Task<ApiResponseDto<IEnumerable<ProdutoDto>?>> GetAllAsync()
    {
        var produtos = await _produtoRepository.GetAllAsync();

        if (produtos == null)
        {
            return  ApiResponseDto<IEnumerable<ProdutoDto>?>.Fail(MessagemErro.ProdutoNaoEncontrado);
        }

        return ApiResponseDto<IEnumerable<ProdutoDto>?>.Ok(produtos.FromEntitiesToDtos());
    }

    public async Task<ApiResponseDto<ProdutoDto?>> GetByIdAsync(int id)
    {
        var produto = await _produtoRepository.GetByIdAsync(id);

        if (produto == null)
        {
            return ApiResponseDto<ProdutoDto>.Fail(MessagemErro.ProdutoNaoEncontrado);
        }

        return ApiResponseDto<ProdutoDto?>.Ok(produto.FromEntityToDto(), string.Empty);
    }

    public async Task<ApiResponseDto<ProdutoDto?>> UpdateAsync(int id, ProdutoDto dto)
    {
        var produtoEntity = await _produtoRepository.GetByIdAsync(id);
        if (produtoEntity == null)
            return ApiResponseDto<ProdutoDto>.Fail(MessagemErro.ProdutoNaoEncontrado);

        else
        {
            produtoEntity.Preco = dto.Preco ?? produtoEntity.Preco;
            produtoEntity.Descricao = dto.Descricao ?? produtoEntity.Descricao; 
            produtoEntity.Nome = dto.Nome ?? produtoEntity.Nome;
            produtoEntity.CategoriaId = dto.Categoria?.Id ?? produtoEntity.CategoriaId;

            var resultado = await _produtoRepository.UpdateAsync(produtoEntity);

            return ApiResponseDto<ProdutoDto?>.Updated(resultado?.FromEntityToDto());
        }
    }
}
