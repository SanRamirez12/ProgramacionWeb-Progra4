
using ExamenProgra4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamenProgra4.Controllers
{
    public class ChoferController : Controller
    {

        private Service services;
        public ChoferController()
        {
            this.services = new Service();
        }

        // GET: ChoferController
        public ActionResult Index()
        {
            var choferes = services.mostrarChoferes();
            return View(choferes);
        }

        // GET: ChoferController/Details/5
        public ActionResult Details(int id)
        {

            var chofer = services.buscarChofer(id);

            //Se manda a llamar el metodo de la clase logica 
            decimal bonificacion = Chofer.CalcularBonificacion(
                chofer.Categoria,
                chofer.Calificacion,
                chofer.FechaRegistro,
                chofer.ComisionGenerada
            );

            //Se pasa el resultado a la vista mediante un ViewBag
            ViewBag.Bonificacion = bonificacion;

            return View(chofer);
        }

        // GET: ChoferController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChoferController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Chofer chofer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.agregarChofer(chofer);
                    return RedirectToAction("Index");
                }
                
            }
            catch
            {
                
            }
            return View();
        }

        // GET: ChoferController/Edit/5
        public ActionResult Edit(int id)
        {
            var choferAnterior = services.buscarChofer(id);
            return View(choferAnterior);
            
        }

        // POST: ChoferController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Chofer chofer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.actualizarChofer(chofer);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                
            }
            return View();
        }

        // GET: ChoferController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var choferEliminado = services.buscarChofer(id);
                return View(choferEliminado);

            }
            catch (Exception)
            {

                return View();
            }
        }

        // POST: ChoferController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var chofer = services.buscarChofer(id);
                services.eliminarChofer(chofer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
