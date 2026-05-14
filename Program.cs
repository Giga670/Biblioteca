using Biblioteca.Models;
using Biblioteca.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddControllersWithViews();

var useSqlite = connectionString.Contains("Data Source", StringComparison.OrdinalIgnoreCase)
    || connectionString.Contains("Filename=", StringComparison.OrdinalIgnoreCase);

if (useSqlite)
{
    var sb = new SqliteConnectionStringBuilder(connectionString);
    var dataSource = sb.DataSource.Trim().Replace('/', Path.DirectorySeparatorChar);
    var resolved = Path.IsPathRooted(dataSource)
        ? dataSource
        : Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, dataSource));
    var dir = Path.GetDirectoryName(resolved);
    if (!string.IsNullOrEmpty(dir))
        Directory.CreateDirectory(dir);
    sb.DataSource = resolved;
    connectionString = sb.ConnectionString;
}

var mysqlServerVersion = new MySqlServerVersion(new Version(8, 0, 36));

if (useSqlite)
    builder.Services.AddDbContext<BibliotecaContext>(o => o.UseSqlite(connectionString));
else
    builder.Services.AddDbContext<BibliotecaContext>(o =>
        o.UseMySql(connectionString, mysqlServerVersion));

builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();

var app = builder.Build();

if (useSqlite)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<BibliotecaContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Biblioteca}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
