namespace Biblioteca.Models;

public class LivroDetalheViewModel
{
    public string Id { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string Imagem { get; set; } = string.Empty;
    public int QtdPaginas { get; set; }
    public int DataPublicacao { get; set; }
    public string Genero { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string Resumo { get; set; } = string.Empty;
    public string AutorId { get; set; } = string.Empty;
}
