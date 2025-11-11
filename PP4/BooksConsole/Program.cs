using System.Text;
using BooksConsole;
using BooksConsole.Models;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = Encoding.UTF8;

using var db = new AppDbContext();

// Verifica si la base de datos está vacía
bool hasData = await db.Authors.AnyAsync();

if (!hasData)
{
    Console.WriteLine("La base de datos está vacía, por lo que será llenada a partir de los datos del archivo CSV.\n");
    Console.WriteLine("Procesando...");

    // Ruta del archivo CSV directamente en la carpeta 'data' del proyecto
    var projectDir = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
    var csvPath = Path.Combine(projectDir, "data", "books.csv");
    if (!File.Exists(csvPath))
    {
        Console.WriteLine($"No se encontró el archivo CSV en: {csvPath}");
        return;
    }

    var authorsCache = new Dictionary<string, Author>(StringComparer.OrdinalIgnoreCase);
    var tagsCache = new Dictionary<string, Tag>(StringComparer.OrdinalIgnoreCase);

    using var sr = new StreamReader(csvPath, Encoding.UTF8);
    await sr.ReadLineAsync(); // Saltar encabezado

    string? line;
    while ((line = await sr.ReadLineAsync()) != null)
    {
        if (string.IsNullOrWhiteSpace(line)) continue;

        var fields = ParseCsvLine(line);
        if (fields.Count < 3) continue;

        string authorName = fields[0].Trim();
        string titleName = fields[1].Trim();
        string tagsRaw = fields[2].Trim();

        if (!authorsCache.TryGetValue(authorName, out var author))
        {
            author = new Author { AuthorName = authorName };
            authorsCache[authorName] = author;
            db.Authors.Add(author);
        }

        var title = new Title { TitleName = titleName, Author = author };
        db.Titles.Add(title);

        var tags = tagsRaw.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var tagName in tags)
        {
            if (!tagsCache.TryGetValue(tagName, out var tag))
            {
                tag = new Tag { TagName = tagName };
                tagsCache[tagName] = tag;
                db.Tags.Add(tag);
            }

            db.TitlesTags.Add(new TitleTag { Title = title, Tag = tag });
        }
    }

    await db.SaveChangesAsync();

    Console.WriteLine("Listo.");
}
else
{
    Console.WriteLine("La base de datos se está leyendo para crear los archivos TSV.\n");
    Console.WriteLine("Procesando...");

    var rows = await (
        from t in db.Titles
        join a in db.Authors on t.AuthorId equals a.AuthorId
        join tt in db.TitlesTags on t.TitleId equals tt.TitleId
        join tag in db.Tags on tt.TagId equals tag.TagId
        select new { a.AuthorName, t.TitleName, tag.TagName }
    ).ToListAsync();

    var grouped = rows.GroupBy(r => char.ToUpperInvariant(r.AuthorName[0]));

    // Ruta directa a la carpeta 'data' del proyecto (no /bin)
    var projectDir = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
    var dataDir = Path.Combine(projectDir, "data");
    if (!Directory.Exists(dataDir))
        Directory.CreateDirectory(dataDir);

    foreach (var g in grouped)
    {
        string fileName = Path.Combine(dataDir, $"{g.Key}.tsv");
        using var sw = new StreamWriter(fileName, false, Encoding.UTF8);
        sw.WriteLine("AuthorName\tTitleName\tTagName");

        foreach (var r in g)
            sw.WriteLine($"{r.AuthorName}\t{r.TitleName}\t{r.TagName}");
    }

    Console.WriteLine("Listo.");
}

static List<string> ParseCsvLine(string line)
{
    var result = new List<string>();
    var sb = new StringBuilder();
    bool inQuotes = false;

    for (int i = 0; i < line.Length; i++)
    {
        char c = line[i];
        if (c == '\"') inQuotes = !inQuotes;
        else if (c == ',' && !inQuotes)
        {
            result.Add(sb.ToString());
            sb.Clear();
        }
        else sb.Append(c);
    }

    result.Add(sb.ToString());
    return result;
}
