using GestaoCatalogo.Application.Dtos;
using GestaoCatalogo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestaoCatalogo.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProdutoController(IProdutoService produtoService) : ControllerBase
{
    private readonly IProdutoService _produtoService = produtoService;

    [HttpPost]
    [Route("criar")]
    public async Task<IActionResult> Create([FromBody] ProdutoDto dto)
    {
        await _produtoService.AddAsync(dto);

        return Ok();
    }

    [HttpPut]
    [Route("atualizar")]
    public async Task<IActionResult> Update([FromBody] ProdutoDto dto)
    {
        await _produtoService.UpdateAsync(dto);

        return Ok();
    }

    [HttpGet]
    [Route("produtos")]
    public async Task<IActionResult> ObterProdutos()
    {
        var produtos = await _produtoService.GetAllAsync();

        return Ok(produtos);
    }
}
