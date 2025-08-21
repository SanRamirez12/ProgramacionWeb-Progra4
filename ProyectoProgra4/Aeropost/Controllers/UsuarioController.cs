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
                var usuario = services.buscarUsuario(id);
                return View(usuario);
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
                // Estos campos NO se editan en esta vista:
                ModelState.Remove(nameof(Usuario.Username));
                ModelState.Remove(nameof(Usuario.Password));
                ModelState.Remove(nameof(Usuario.ConfirmPassword));

                if (!ModelState.IsValid)
                    return View(usuario);

                var original = services.buscarUsuario(usuario.Id);

                // Actualiza SOLO datos permitidos
                original.Nombre = usuario.Nombre;
                original.Genero = usuario.Genero;
                original.FechaRegistro = usuario.FechaRegistro == default
                                          ? original.FechaRegistro
                                          : usuario.FechaRegistro;
                original.Estado = usuario.Estado;
                original.Correo = usuario.Correo;
                // Si la cédula no se edita, no la toques (o déjala readonly en la vista)
                // original.Cedula = original.Cedula;

                services.actualizarUsuario(original); // método que no toca user/pass
                return RedirectToAction(nameof(Index));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var detalles = string.Join("; ",
                    ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors)
                      .Select(v => $"{v.PropertyName}: {v.ErrorMessage}"));
                ModelState.AddModelError(string.Empty, "Error al actualizar: " + detalles);
                return View(usuario);
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
            HttpContext.Session.Clear();
            return View();
        }

        // POST: UsuarioController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            try
            {
                var usuarioLogueado = services.login(username, password); // registra inicio adentro

                //Variables de sesion
                HttpContext.Session.SetString("VarSesion_NombreUsuario", usuarioLogueado.Nombre);
                HttpContext.Session.SetString("VarSesion_Username", usuarioLogueado.Username);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // POST: UsuarioController/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            var username = HttpContext.Session.GetString("VarSesion_Username");

            if (!string.IsNullOrWhiteSpace(username))
                services.registrarSalida(username); // Bitácora: marcar fecha de salida

            HttpContext.Session.Clear(); // limpiar variables de sesión
            return RedirectToAction("Login", "Usuario");
        }



        // GET
        public ActionResult ChangePassword(int id)
        {
            try
            {
                var u = services.buscarUsuario(id);
                return View(new Usuario { Id = u.Id }); // Solo Id para el form
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(Usuario usuario)
        {
            try
            {
                // Validación MANUAL (lado servidor)
                if (string.IsNullOrWhiteSpace(usuario.Password))
                    ModelState.AddModelError(nameof(usuario.Password), "La contraseña es obligatoria.");

                if ((usuario.Password ?? "").Length < 8)
                    ModelState.AddModelError(nameof(usuario.Password), "La contraseña debe tener al menos 8 caracteres.");

                if (usuario.Password != usuario.ConfirmPassword)
                    ModelState.AddModelError(nameof(usuario.ConfirmPassword), "Las contraseñas no coinciden.");

                // Evitar que otros [Required] del modelo tumben esta pantalla
                ModelState.Remove(nameof(Usuario.Nombre));
                ModelState.Remove(nameof(Usuario.Cedula));
                ModelState.Remove(nameof(Usuario.Genero));
                ModelState.Remove(nameof(Usuario.FechaRegistro));
                ModelState.Remove(nameof(Usuario.Estado));
                ModelState.Remove(nameof(Usuario.Correo));
                ModelState.Remove(nameof(Usuario.Username));

                if (!ModelState.IsValid)
                    return View(usuario);

                // Cargar entidad real y cambiar SOLO el password
                var original = services.buscarUsuario(usuario.Id);

                // (Recomendado) hashear aquí antes de guardar
                // original.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
                original.Password = usuario.Password;

                services.actualizarPassword(original.Id, original.Password);

                TempData["Ok"] = "Contraseña actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var detalles = string.Join("; ",
                    ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors)
                      .Select(v => $"{v.PropertyName}: {v.ErrorMessage}"));
                ModelState.AddModelError(string.Empty, "Error de validación EF: " + detalles);
                return View(usuario);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al actualizar la contraseña: {ex.Message}");
                return View(usuario);
            }
        }







    }
}
