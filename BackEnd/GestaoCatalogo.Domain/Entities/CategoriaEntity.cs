namespace GestaoCatalogo.Domain.Entities;

public class CategoriaEntity
{
    public int Id { get; set; }
    public string Nome { get; set; } 
    public string Descricao { get; set; }

    public ICollection<ProdutoEntity>? Produtos { get; set; }
}
