namespace GestaoCatalogo.Application.Dtos;

public class CategoriaDto
{
    public int? Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }

    public ICollection<ProdutoDto>? Produtos { get; set; }
}
