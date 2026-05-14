using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Biblioteca.Models;

public class BibliotecaContext : DbContext
{
    public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
        : base(options)
    {
    }

    public DbSet<Livro> Livros => Set<Livro>();
    public DbSet<Autor> Autores => Set<Autor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var comparadorLista = new ValueComparer<List<string>>(
            (a, b) => (a == null && b == null) || (a != null && b != null && a.SequenceEqual(b)),
            c => c.Aggregate(0, (acc, s) => HashCode.Combine(acc, s.GetHashCode(StringComparison.Ordinal))),
            c => c.ToList());

        modelBuilder.Entity<Autor>()
            .Property(a => a.Obras)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null!),
                v => string.IsNullOrWhiteSpace(v)
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(v)!)
            .Metadata.SetValueComparer(comparadorLista);

        modelBuilder.Entity<Livro>()
            .HasOne(l => l.AutorRegistro)
            .WithMany()
            .HasForeignKey(l => l.IdAutor)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
