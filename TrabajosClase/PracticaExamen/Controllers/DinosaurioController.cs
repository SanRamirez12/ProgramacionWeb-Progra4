using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticaExamen.Models;

namespace PracticaExamen.Controllers
{
    public class DinosaurioController : Controller
    {

        private Service services;
        public DinosaurioController()
        {
            this.services = new Service();
        }

        // GET: DinosaurioController
        public ActionResult Index()
        {
            var dinosaurios = services.mostrarDinosarios();
            return View(dinosaurios);
        }

        // GET: DinosaurioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DinosaurioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DinosaurioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dinosaurio dinosaurio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.agregarDinosaurio(dinosaurio);
                    return RedirectToAction("Index");
                }
                
            }
            catch
            {}
            return View();
        }

        // GET: DinosaurioController/Edit/5
        public ActionResult Edit(int id)
        {
            var dinosaurioAnterior = services.buscarDinosaurio(id);
            return View(dinosaurioAnterior);
        }

        // POST: DinosaurioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dinosaurio dinosaurio)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.actualizarDinosaurio(dinosaurio);
                    return RedirectToAction("Index");
                }
            
            }
            catch
            {
                
            }
            return View();
        }

        // GET: DinosaurioController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var dinosaurioEliminado = services.buscarDinosaurio(id);
                return View(dinosaurioEliminado);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: DinosaurioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var dinosaurio = services.buscarDinosaurio(id);
                services.eliminarDinosaurio(dinosaurio);    
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
