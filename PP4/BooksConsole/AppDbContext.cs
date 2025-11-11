using BooksConsole.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksConsole;

public class AppDbContext : DbContext
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Title> Titles => Set<Title>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<TitleTag> TitlesTags => Set<TitleTag>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Crear la carpeta 'data' directamente en la raíz del proyecto (no en /bin)
        var projectDir = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
        var dataDir = Path.Combine(projectDir, "data");
        if (!Directory.Exists(dataDir))
            Directory.CreateDirectory(dataDir);

        var dbPath = Path.Combine(dataDir, "books.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Nombre de tabla personalizado para TitleTag → TitlesTags
        modelBuilder.Entity<TitleTag>().ToTable("TitlesTags");

        // Orden de columnas en Title
        modelBuilder.Entity<Title>(entity =>
        {
            entity.Property(e => e.TitleId).HasColumnOrder(0);
            entity.Property(e => e.AuthorId).HasColumnOrder(1);
            entity.Property(e => e.TitleName).HasColumnOrder(2);
        });

        // Propiedades requeridas
        modelBuilder.Entity<Author>(e =>
        {
            e.Property(x => x.AuthorName).IsRequired();
        });
        modelBuilder.Entity<Title>(e =>
        {
            e.Property(x => x.TitleName).IsRequired();
        });
        modelBuilder.Entity<Tag>(e =>
        {
            e.Property(x => x.TagName).IsRequired();
        });
        modelBuilder.Entity<TitleTag>(e =>
        {
            e.Property(x => x.TitleId).IsRequired();
            e.Property(x => x.TagId).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
