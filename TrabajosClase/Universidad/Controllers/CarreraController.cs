using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universidad.Models;

namespace Universidad.Controllers
{
    public class CarreraController : Controller
    {

        private Service services;
        public CarreraController()
        {
            this.services = new Service();
        }

        // GET: CarreraController
        public ActionResult Index()
        {
            var carreras = services.mostrarCarreras();
            return View(carreras);
        }

        // GET: CarreraController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarreraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarreraController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Carrera carrera)
        {
            try
            {
                if (ModelState.IsValid) {
                    services.agregarCarrera(carrera);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
               
            }
            return View();
        }

        // GET: CarreraController/Edit/5
        public ActionResult Edit(int id)
        {
            var carreraAnterior = services.buscarCarrera(id);
            return View(carreraAnterior);
        }

        // POST: CarreraController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Carrera carrera)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.actualizarCarrera(carrera);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                
            }
            return View();
        }

        // GET: CarreraController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var carreraEliminada = services.buscarCarrera(id);
                return View(carreraEliminada);

            }
            catch (Exception)
            {

                return View();
            }
        }

        // POST: CarreraController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var carrera = services.buscarCarrera(id);
                services.eliminarCarrera(carrera);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }

}

