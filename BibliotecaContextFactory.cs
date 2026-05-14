using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Biblioteca;

/// <summary>
/// Usada pelo CLI (<c>dotnet ef database update</c>) para aplicar migrações ao MySQL
/// definido em <c>appsettings.json</c>, independentemente do ambiente de execução da app.
/// </summary>
public class BibliotecaContextFactory : IDesignTimeDbContextFactory<BibliotecaContext>
{
    public BibliotecaContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Defina ConnectionStrings:DefaultConnection em appsettings.json.");

        // Versão fixa: evita AutoDetect (que precisa de ligação ao MySQL) ao gerar migrações com dotnet-ef.
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
        var options = new DbContextOptionsBuilder<BibliotecaContext>()
            .UseMySql(connectionString, serverVersion)
            .Options;

        return new BibliotecaContext(options);
    }
}
