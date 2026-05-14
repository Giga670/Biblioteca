using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories;

public class LivroRepository : ILivroRepository
{
    readonly BibliotecaContext _context;

    public LivroRepository(BibliotecaContext context)
    {
        _context = context;
    }

    public async Task<List<Livro>> BuscarTodosLivrosAsync() =>
        await _context.Livros.AsNoTracking().OrderBy(l => l.Titulo).ToListAsync();

    public async Task<Livro?> BuscarLivroPorIdAsync(int id) =>
        await _context.Livros.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);

    public async Task<bool> CriarLivroAsync(Livro livro)
    {
        await _context.Livros.AddAsync(livro);
        await _context.SaveChangesAsync();
        return true;
    }
}

public interface ILivroRepository
{
    Task<List<Livro>> BuscarTodosLivrosAsync();
    Task<Livro?> BuscarLivroPorIdAsync(int id);
    Task<bool> CriarLivroAsync(Livro livro);
}
