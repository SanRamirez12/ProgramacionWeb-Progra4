using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Universidad.Models;

namespace Universidad.Controllers
{
    public class EstudianteController : Controller
    {
        private Service services;

        public EstudianteController() 
        { //Creamos el constructor del controlador y le indicamos con el service hacia que base de datos nos dirigimos
            this.services = new Service();
        }
        // GET: EstudianteController
        public ActionResult Index()
        { //Se conecta el modelo con el view a partir del service llamado en el controller
            var estudiantes = services.mostrarEstudiantes(); 
            return View(estudiantes);
        }

        // GET: EstudianteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstudianteController/Create
        public ActionResult Create()
        {//Cargar el formulario la primera vez en blanco, tipo generar la vista
            return View();
        }

        // POST: EstudianteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Estudiante estudiante)
        {//Este ya crea los valores de una y los trae
            try
            {
                if (ModelState.IsValid)
                {
                    services.agregarEstudiante(estudiante);
                    return RedirectToAction("Index");
                }
            }
            catch
            {   
            }
            return View();
        }

        // GET: EstudianteController/Edit/5
        public ActionResult Edit(int id)
        {
            var estudianteAnterior = services.buscarEstudiante(id);
            return View(estudianteAnterior);
        }

        // POST: EstudianteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Estudiante estudiante)
        {
            try
            {   if (ModelState.IsValid)
                {
                    services.actualizarEstudiante(estudiante);
                    return RedirectToAction("Index");
                }
                
            }
            catch
            {
                
            }
            return View();
        }

        // GET: EstudianteController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var estudianteEliminado = services.buscarEstudiante(id);
                services.eliminarEstudiante(estudianteEliminado);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return View();
            }
            
        }

        // POST: EstudianteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
