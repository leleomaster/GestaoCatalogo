using GestaoCatalogo.Application.Dtos;
using GestaoCatalogo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestaoCatalogo.Api.Controllers;

[ApiController]
public class CategoriaController(ICategoriaService categoriaService) : ControllerBase
{
    private readonly ICategoriaService _categoriaService = categoriaService;

    [HttpPost]
    [Route("api/categorias")]
    public async Task<IActionResult> Create([FromBody] CategoriaDto dto)
    {
        var response = await _categoriaService.AddAsync(dto);

        return Ok(response);
    }

    [HttpPut]
    [Route("api/categorias/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoriaDto dto)
    {
        var response = await _categoriaService.UpdateAsync(id, dto);

        return Ok(response);
    }

    [HttpGet]
    [Route("api/categorias")]
    public async Task<IActionResult> ObterCategorias()
    {
        var response = await _categoriaService.GetAllAsync();

        return Ok(response);
    }

    [HttpDelete]
    [Route("api/categorias/{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var response = await _categoriaService.DeleteAsync(id);

        if(response?.Success == false)
            return Conflict(response);

        return Ok(response);
    }
}
