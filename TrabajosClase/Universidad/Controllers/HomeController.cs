using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Universidad.Models;

namespace Universidad.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Validacion par que no se acceda a index mediante url interna(debe hacerse en todas las vistas)
            var NombreUsuario = HttpContext.Session.GetString("VarSesion_NombreUsuario");//variable del usarioController del metodo POST del login
            if (NombreUsuario != null) 
                ViewBag.NombreUsuario = NombreUsuario;
            else
                return RedirectToAction("Login","Usuario");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Carreras()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
