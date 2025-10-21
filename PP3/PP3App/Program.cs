using System.Text;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// -------------------- ROOT --------------------
app.MapGet("/", () => Results.Redirect("/swagger"));

// -------------------- /include --------------------
app.MapPost("/include/{position:int}", (
    [FromRoute] int position,
    [FromQuery] string value,
    [FromForm] string text,
    HttpRequest request) =>
{
    bool xml = false;
    if (request.Headers.TryGetValue("xml", out var xmlHeader))
        bool.TryParse(xmlHeader, out xml);

    if (position < 0)
        return Results.BadRequest(new { error = "'position' must be 0 or higher" });
    if (string.IsNullOrWhiteSpace(value))
        return Results.BadRequest(new { error = "'value' cannot be empty" });
    if (string.IsNullOrWhiteSpace(text))
        return Results.BadRequest(new { error = "'text' cannot be empty" });

    var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    position = Math.Min(position, words.Count);
    words.Insert(position, value);
    var newSentence = string.Join(' ', words);

    var result = new Result(text, newSentence);

    return xml
        ? Results.Content(XmlHelper.ToXml(result), "application/xml")
        : Results.Json(new { ori = result.Ori, @new = result.New });
}).DisableAntiforgery();

// -------------------- /replace --------------------
app.MapPut("/replace/{length:int}", (
    [FromRoute] int length,
    [FromQuery] string value,
    [FromForm] string text,
    HttpRequest request) =>
{
    bool xml = false;
    if (request.Headers.TryGetValue("xml", out var xmlHeader))
        bool.TryParse(xmlHeader, out xml);

    if (length <= 0)
        return Results.BadRequest(new { error = "'length' must be higher than 0" });
    if (string.IsNullOrWhiteSpace(value))
        return Results.BadRequest(new { error = "'value' cannot be empty" });
    if (string.IsNullOrWhiteSpace(text))
        return Results.BadRequest(new { error = "'text' cannot be empty" });

    var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var replaced = words.Select(w => w.Length == length ? value : w);
    var newSentence = string.Join(' ', replaced);
    var result = new Result(text, newSentence);

    return xml
        ? Results.Content(XmlHelper.ToXml(result), "application/xml")
        : Results.Json(new { ori = result.Ori, @new = result.New });
}).DisableAntiforgery();

// -------------------- /erase --------------------
app.MapDelete("/erase/{length:int}", (
    [FromRoute] int length,
    [FromForm] string text,
    HttpRequest request) =>
{
    bool xml = false;
    if (request.Headers.TryGetValue("xml", out var xmlHeader))
        bool.TryParse(xmlHeader, out xml);

    if (length <= 0)
        return Results.BadRequest(new { error = "'length' must be higher than 0" });
    if (string.IsNullOrWhiteSpace(text))
        return Results.BadRequest(new { error = "'text' cannot be empty" });

    var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var filtered = words.Where(w => w.Length != length);
    var newSentence = string.Join(' ', filtered);
    var result = new Result(text, newSentence);

    return xml
        ? Results.Content(XmlHelper.ToXml(result), "application/xml")
        : Results.Json(new { ori = result.Ori, @new = result.New });
}).DisableAntiforgery();

app.Run();

// ========================================================
// 🔽 Helpers y modelos deben ir fuera del top-level code 🔽
// ========================================================

public class Result
{
    public string Ori { get; set; } = "";
    public string New { get; set; } = "";

    public Result() { }

    public Result(string ori, string @new)
    {
        Ori = ori;
        New = @new;
    }
}

public static class XmlHelper
{
    public static string ToXml<T>(T obj)
    {
        var serializer = new XmlSerializer(typeof(T));
        using var ms = new MemoryStream();
        serializer.Serialize(ms, obj);
        return Encoding.UTF8.GetString(ms.ToArray());
    }
}
