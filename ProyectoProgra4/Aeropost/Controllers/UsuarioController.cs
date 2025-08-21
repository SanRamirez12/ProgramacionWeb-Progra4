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
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                // No queremos validar 'Id' en Create:
                ModelState.Remove(nameof(Usuario.Id));

                if (usuario.FechaRegistro == default)
                    usuario.FechaRegistro = DateTime.Now;

                //Validaciones ConfirmPassword:
                if (usuario.Password != usuario.ConfirmPassword)
                    ModelState.AddModelError(nameof(usuario.ConfirmPassword), "Las contraseñas no coinciden.");

                if (!ModelState.IsValid)
                    return View(usuario);

                services.agregarUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
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
                // Validación por si faltan scripts en el usuario
                if (usuario.Password != usuario.ConfirmPassword)
                    ModelState.AddModelError(nameof(usuario.ConfirmPassword), "Las contraseñas no coinciden.");

                if (!ModelState.IsValid)
                    return View(usuario);

                // Recuperar el original y solo actualizar la contraseña
                var original = services.buscarUsuario(usuario.Id);
                original.Password = usuario.Password; // aquí idealmente hash

                services.actualizarUsuario(original);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar: {ex.Message}");
                return View(usuario);
            }
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
