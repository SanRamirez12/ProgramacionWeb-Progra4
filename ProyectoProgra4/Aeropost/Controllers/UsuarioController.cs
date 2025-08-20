using Aeropost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aeropost.Controllers
{
    public class UsuarioController : Controller
    {
        // Instanciamos service
        private Service services;
        public UsuarioController()
        {
            this.services = new Service();
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            var usuarios = services.mostrarUsuarios();
            return View(usuarios);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var usuario = services.buscarUsuario(id);
                return View(usuario);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario /*, string ConfirmPassword si luego vuelves a usarlo*/)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // Muestra exactamente qué falló
                    ViewBag.ModelErrors = ModelState
                        .Where(kvp => kvp.Value.Errors.Count > 0)
                        .Select(kvp => $"{kvp.Key}: {string.Join(" | ", kvp.Value.Errors.Select(e => e.ErrorMessage))}")
                        .ToList();
                    return View(usuario);
                }

                if (usuario.FechaRegistro == default)
                    usuario.FechaRegistro = DateTime.Now;

                services.agregarUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Muestra el error real (por ej. restricción de BD)
                ModelState.AddModelError(string.Empty, $"Error al crear el usuario: {ex.Message}");
                return View(usuario);
            }
        }



        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var usuarioAnterior = services.buscarUsuario(id);
                return View(usuarioAnterior);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.actualizarUsuario(usuario);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                // Log / manejar error si es necesario
            }
            return View(usuario);
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var usuario = services.buscarUsuario(id);
                return View(usuario);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var usuario = services.buscarUsuario(id);
                services.eliminarUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //Metodos de login
        // GET: UsuarioController/Login
        public ActionResult Login()
        {
            HttpContext.Session.Clear(); //Limpiar las variables de session
            return View();
        }

        // POST: UsuarioController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            try
            {
                var usuarioLogueado = services.login(username, password);
                HttpContext.Session.SetString("VarSesion_NombreUsuario", usuarioLogueado.Nombre);//creamos la variable de sesion
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }

        }
    }
}
