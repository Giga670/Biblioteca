using Biblioteca.Models;

namespace Biblioteca.Services;

public static class AutorCatalog
{
    private static readonly Dictionary<string, Autor> PorNome =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["J.K. Rowling"] = new Autor
            {
                Id = 1,
                Nome = "J.K. Rowling",
                Nacionalidade = "Britânica",
                Periodo = "1965–",
                Imagem = "book.svg",
                Biografia = "Autora britânica, conhecida mundialmente pela série Harry Potter.",
                Obras = new List<string>
                {
                    "Harry Potter e a Pedra Filosofal",
                    "Harry Potter e a Câmara Secreta"
                }
            },
            ["Antoine de Saint-Exupéry"] = new Autor
            {
                Id = 2,
                Nome = "Antoine de Saint-Exupéry",
                Nacionalidade = "Francesa",
                Periodo = "1900–1944",
                Imagem = "book.svg",
                Biografia = "Aviador e escritor francês, autor de O Pequeno Príncipe.",
                Obras = new List<string> { "O Pequeno Príncipe", "Piloto de Guerra" }
            }
        };

    public static Autor? PorNomeAutor(string? nome) =>
        string.IsNullOrWhiteSpace(nome) ? null
        : PorNome.TryGetValue(nome.Trim(), out var autor) ? autor : null;
}
