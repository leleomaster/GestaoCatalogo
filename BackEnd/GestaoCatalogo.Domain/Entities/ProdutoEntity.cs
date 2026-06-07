namespace GestaoCatalogo.Domain.Entities;

public class ProdutoEntity
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }

    public int CategoriaId { get; set; }
    public CategoriaEntity? Categoria { get; set; }
}
