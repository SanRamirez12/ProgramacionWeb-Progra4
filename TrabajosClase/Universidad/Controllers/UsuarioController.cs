using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universidad.Models;

namespace Universidad.Controllers
{
    public class UsuarioController : Controller
    {
        private Service services;
        public UsuarioController()
        {
            this.services = new Service();
        }// GET: UsuarioController
        public ActionResult Index()
        {
            var usuarios = services.mostrarUsuarios();
            return View(usuarios);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                if (ModelState.IsValid)
                {
                    services.agregarUsuario(usuario);
                    return RedirectToAction("Index");
                }
            }
            catch
            {

            }
            return View();
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            var usuarioAnterior = services.buscarUsuario(id);
            return View(usuarioAnterior);
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
                    return RedirectToAction("Index");
                }

            }
            catch
            {

            }
            return View();
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var usuarioEliminado = services.buscarUsuario(id);
                return View(usuarioEliminado);
            }
            catch (Exception)
            {
                return View();
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
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //Metodos de login
        // GET: UsuarioController/Login
        public ActionResult Login()
        {
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
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex) 
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
            
        }


    }
}