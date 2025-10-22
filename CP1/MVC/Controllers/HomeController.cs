using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(TheModel model)
    {
        ViewBag.Valid = ModelState.IsValid;
        if (ViewBag.Valid)
        {
            var charArray = model.Phrase
            .Where(c => !char.IsWhiteSpace(c)) // filtra los espacios con  LINQ https://learn.microsoft.com/en-us/dotnet/csharp/linq
            .ToList();

            foreach (var c in charArray)
            {
                if (!model.Counts.ContainsKey(c))
                    model.Counts[c] = 0;

                model.Counts[c]++;

                model.Lower += char.ToLower(c);
                model.Upper += char.ToUpper(c);
            }

        }
        return View(model);
    }
}
