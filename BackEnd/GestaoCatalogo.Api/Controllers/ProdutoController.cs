using Azure;
using GestaoCatalogo.Application.Dtos;
using GestaoCatalogo.Application.Interfaces;
using GestaoCatalogo.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoCatalogo.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class ProdutoController(IProdutoService produtoService) : ControllerBase
{
    private readonly IProdutoService _produtoService = produtoService;

    [HttpPost]
    [Route("api/produtos")]
    public async Task<IActionResult> Create([FromBody] ProdutoDto dto)
    {
        var response = await _produtoService.AddAsync(dto);

        return Ok(response);
    }

    [HttpPut]
    [Route("api/produtos/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProdutoDto dto)
    {
        var response = await _produtoService.UpdateAsync(id, dto);

        return Ok(response);
    }

    [HttpGet]
    [Route("api/produtos")]
    public async Task<IActionResult> ObterProdutos()
    {
        var response = await _produtoService.GetAllAsync();

        return Ok(response);
    }

    [HttpDelete]
    [Route("api/produtos/{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var response = await _produtoService.DeleteAsync(id);

        if (response?.Success == false)
            return Conflict(response);

        return Ok(response);
    }
}
