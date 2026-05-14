namespace Biblioteca.Models;

public class Livro
{
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public string? Imagem { get; set; }
    public string? Autor { get; set; }
    public int QtdPaginas { get; set; }
    public int DataPublicacao { get; set; }
    public string? Genero { get; set; }
    public string? Resumo { get; set; }
    public int? IdAutor { get; set; }
    public Autor? AutorRegistro { get; set; }
}
