using Biblioteca.Models;
using Biblioteca.Repositories;
using Biblioteca.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Biblioteca.Controllers;

public class BibliotecaController : Controller
{
    readonly ILivroRepository _livroRepository;
    readonly IAutorRepository _autorRepository;

    public BibliotecaController(ILivroRepository livroRepository, IAutorRepository autorRepository)
    {
        _livroRepository = livroRepository;
        _autorRepository = autorRepository;
    }

    public async Task<IActionResult> Index()
    {
        var todos = await _livroRepository.BuscarTodosLivrosAsync();
        return View(todos.Take(6).ToList());
    }

    public async Task<IActionResult> Catalogo()
    {
        var livros = await _livroRepository.BuscarTodosLivrosAsync();
        return View(livros);
    }

    public async Task<IActionResult> LivroDetalhe(int id)
    {
        var livro = await _livroRepository.BuscarLivroPorIdAsync(id);
        if (livro is null)
            return NotFound();
        return View(livro);
    }

    public async Task<IActionResult> AutorDetalhe(int id)
    {
        var autor = await _autorRepository.BuscarPorIdAsync(id);
        if (autor is not null)
            return View(autor);

        var livro = await _livroRepository.BuscarLivroPorIdAsync(id);
        if (livro?.IdAutor is int aid && aid > 0)
            autor = await _autorRepository.BuscarPorIdAsync(aid);
        if (autor is null && livro is not null && !string.IsNullOrWhiteSpace(livro.Autor))
            autor = await _autorRepository.BuscarPorNomeAsync(livro.Autor.Trim());
        if (autor is null && livro is not null)
            autor = AutorCatalog.PorNomeAutor(livro.Autor);
        if (autor is null)
            return NotFound();
        return View(autor);
    }

    public IActionResult CadastroLivroAutor() => View();

    [HttpGet]
    public async Task<IActionResult> CriarLivro()
    {
        await PreencherSelectAutoresAsync();
        return View(new Livro());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CriarLivro(
        [Bind(nameof(Livro.Titulo), nameof(Livro.Imagem), nameof(Livro.Autor), nameof(Livro.QtdPaginas),
            nameof(Livro.DataPublicacao), nameof(Livro.Genero), nameof(Livro.Resumo), nameof(Livro.IdAutor))]
        Livro livro)
    {
        if (livro.IdAutor is int idAutor && idAutor > 0)
        {
            var autor = await _autorRepository.BuscarPorIdAsync(idAutor);
            if (autor is not null)
                livro.Autor = autor.Nome;
        }

        if (!ModelState.IsValid)
        {
            await PreencherSelectAutoresAsync();
            return View(livro);
        }

        livro.Id = 0;
        await _livroRepository.CriarLivroAsync(livro);
        return RedirectToAction(nameof(Catalogo));
    }

    [HttpGet]
    public IActionResult CriarAutor() => View(new CriarAutorFormModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CriarAutor(CriarAutorFormModel form)
    {
        if (!ModelState.IsValid)
            return View(form);

        var obras = (form.ObrasTexto ?? "")
            .Split(new[] { '\r', '\n', ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .Where(s => s.Length > 0)
            .ToList();

        var autor = new Autor
        {
            Nome = form.Nome.Trim(),
            Nacionalidade = form.Nacionalidade.Trim(),
            Periodo = form.Periodo.Trim(),
            Imagem = form.Imagem.Trim(),
            Biografia = form.Biografia.Trim(),
            Obras = obras
        };

        await _autorRepository.CriarAsync(autor);
        return RedirectToAction(nameof(ListaAutores));
    }

    public async Task<IActionResult> ListaAutores()
    {
        var lista = await _autorRepository.BuscarTodosAsync();
        return View(lista);
    }

    async Task PreencherSelectAutoresAsync()
    {
        var autores = await _autorRepository.BuscarTodosAsync();
        var itens = new List<SelectListItem>
        {
            new() { Value = "", Text = "— Nenhum —" }
        };
        itens.AddRange(autores.Select(a =>
            new SelectListItem { Value = a.Id.ToString(), Text = a.Nome }));
        ViewBag.AutoresItens = itens;
    }
}
