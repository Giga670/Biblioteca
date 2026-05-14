using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repositories;

public class AutorRepository : IAutorRepository
{
    readonly BibliotecaContext _context;

    public AutorRepository(BibliotecaContext context)
    {
        _context = context;
    }

    public async Task<List<Autor>> BuscarTodosAsync() =>
        await _context.Autores.AsNoTracking().OrderBy(a => a.Nome).ToListAsync();

    public async Task<Autor?> BuscarPorIdAsync(int id) =>
        await _context.Autores.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Autor?> BuscarPorNomeAsync(string nome)
    {
        nome = nome.Trim();
        return await _context.Autores.AsNoTracking()
            .FirstOrDefaultAsync(a => a.Nome.ToLower() == nome.ToLower());
    }

    public async Task<bool> CriarAsync(Autor autor)
    {
        await _context.Autores.AddAsync(autor);
        await _context.SaveChangesAsync();
        return true;
    }
}

public interface IAutorRepository
{
    Task<List<Autor>> BuscarTodosAsync();
    Task<Autor?> BuscarPorIdAsync(int id);
    Task<Autor?> BuscarPorNomeAsync(string nome);
    Task<bool> CriarAsync(Autor autor);
}
