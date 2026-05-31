namespace GestaoCatalogo.Domain.Entities;

public class CategoriaEntity
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;

    public ICollection<ProdutoEntity> Produtos { get; set; } = new List<ProdutoEntity>();
}
