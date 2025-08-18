using Aeropost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aeropost.Controllers
{
    public class PaqueteController : Controller
    {
        private Service services;
        public PaqueteController()
        {
            this.services = new Service();
        }
        // GET: PaqueteController
        public ActionResult Index()
        {
            var paquetes = services.mostrarPaquete();
            return View(paquetes);
        }

        // GET: PaqueteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaqueteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaqueteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Paquete paquete)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.agregarPaquete(paquete);
                    return RedirectToAction("Index");
                }
            }
            catch
            {

            }
            return View();
        }

        // GET: PaqueteController/Edit/5
        public ActionResult Edit(int id)
        {
            var paqueteAnterior = services.buscarPaquete(id);
            return View(paqueteAnterior);
        }

        // POST: PaqueteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Paquete paquete)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.actualizarPaquete(paquete);
                    return RedirectToAction("Index");
                }

            }
            catch
            {

            }
            return View();
        }

        // GET: PaqueteController/Delete/5
        public ActionResult Delete(int id)
        {

            try
            {
                var paqueteEliminado = services.buscarPaquete(id);
                return View(paqueteEliminado);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: PaqueteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var paquete = services.buscarPaquete(id);
                services.eliminarPaquete(paquete);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
