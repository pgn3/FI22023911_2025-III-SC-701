using Microsoft.AspNetCore.Mvc;
using PP2App.Models;
using PP2App.Helpers;

namespace PP2App.Controllers
{
    // Controllador que llama al helper y retorna la vista con los resultados
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(BinaryModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            // Utilizo el helper para procesar los datos y obtener los resultados, así trato de respetar principios de SOLID y mantener el controlador limpio
            var results = BinaryHelper.Process(model.A!, model.B!);
            ViewBag.Results = results;
            return View(model);
        }
    }
}
