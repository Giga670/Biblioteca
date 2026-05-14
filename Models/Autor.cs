namespace Biblioteca.Models;

public class Autor
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Nacionalidade { get; set; } = string.Empty;
    public string Periodo { get; set; } = string.Empty;
    public string Imagem { get; set; } = string.Empty;
    public string Biografia { get; set; } = string.Empty;
    public List<string> Obras { get; set; } = new();
}
