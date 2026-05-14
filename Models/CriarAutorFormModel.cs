namespace Biblioteca.Models;

public class CriarAutorFormModel
{
    [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = string.Empty;

    public string Nacionalidade { get; set; } = string.Empty;
    public string Periodo { get; set; } = string.Empty;
    public string Imagem { get; set; } = string.Empty;
    public string Biografia { get; set; } = string.Empty;
    public string? ObrasTexto { get; set; }
}
