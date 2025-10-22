using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var list = new List<object>();

app.MapGet("/", () => Results.Redirect("/swagger"));

// POST con header opcional xml
// ChatGPT me ayudo a reimplementar esta parte 
app.MapPost("/", (HttpRequest request) =>
{
    bool xml = false;

    // Si existe el header "xml", intenta convertirlo a boolean
    if (request.Headers.TryGetValue("xml", out var xmlHeader))
        bool.TryParse(xmlHeader, out xml);

    if (xml)
    {
        var xmlResult = XmlHelper.ToXml(list);
        return Results.Content(xmlResult, "application/xml");
    }

    return Results.Json(list);
}).DisableAntiforgery();



app.MapPut("/", ([FromForm] int quantity, [FromForm] string type) =>
{
    var random = new Random();

    if (quantity <= 0)
        return Results.BadRequest(new { error = "La cantidad debe ser mayor a 0" });

    if (type == "int")
    {
        for (int i = 0; i < quantity; i++)
            list.Add(random.Next());
    }
    else if (type == "float")
    {
        for (int i = 0; i < quantity; i++)
            list.Add(random.NextSingle());
    }
    else
    {
        return Results.BadRequest(new { error = "El tipo debe ser int o float" });
    }

    return Results.Ok(new { message = $"{quantity} elementos agregados como {type}" });
}).DisableAntiforgery();

app.MapDelete("/", ([FromForm] int quantity) =>
{
    if (quantity <= 0)
        return Results.BadRequest(new { error = "La cantidad debe ser mayor a 0" });

    if (quantity > list.Count)
        return Results.BadRequest(new { error = "La cantidad a eliminar es mayor a la cantidad de elementos en la lista" });

    for (int i = 0; i < quantity; i++)
        list.RemoveAt(0);

    return Results.Ok(new { message = $"{quantity} elementos eliminados" });
}).DisableAntiforgery();

app.MapPatch("/", () =>
{
    list.Clear();
    return Results.Ok();
}).DisableAntiforgery();

app.Run();

// Sacado de la practica PP3
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
