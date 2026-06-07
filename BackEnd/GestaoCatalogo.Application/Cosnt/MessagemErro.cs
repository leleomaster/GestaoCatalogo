namespace GestaoCatalogo.Application.Cosnt;

public static class MessagemErro
{
    #region Categoria
    public const string CategoriaNaoEncontrada = "Categoria não encontrada.";

    #endregion

    #region produto 
    public const string ProdutoNaoEncontrado = "Produto não encontrado.";
    public const string ProdutoAssociadoCategoria = "Não é possível excluir uma categoria que possua produtos vinculados.";

    #endregion
}
