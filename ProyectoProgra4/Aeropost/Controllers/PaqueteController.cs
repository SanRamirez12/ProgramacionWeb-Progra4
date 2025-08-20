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
                if (!ModelState.IsValid) return View(paquete);

                //En caso de que la fecha de registro venga vacía desde el form le asigna de golpe la fecha
                if (paquete.FechaRegistro == default) paquete.FechaRegistro = DateTime.Now;

                const int maxIntentos = 10;
                for (int i = 0; i < maxIntentos; i++)
                {
                    paquete.GenerarTracking(); // genera con el numero de tracking unico

                    // Verifica unicidad en la BD
                    if (!services.ExisteTracking(paquete.NumeroTracking))
                    {
                        services.agregarPaquete(paquete);
                        return RedirectToAction(nameof(Index));
                    }
                }

                // Si llegamos aquí, no logramos generar un tracking único
                ModelState.AddModelError(nameof(Paquete.NumeroTracking),"No fue posible generar un número de tracking único. Intente nuevamente.");
                return View(paquete);
            }
            catch (Exception ex)
            {
                // Log opcional ex
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear el paquete.");
                return View(paquete);
            }
        }


        // GET: PaqueteController/Edit/5
        public ActionResult Edit(int id)
        {
            var paqueteAnterior = services.buscarPaquete(id);
            if (paqueteAnterior == null) return NotFound();
            return View(paqueteAnterior);
        }

        // POST: PaqueteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Paquete paquete)
        {
            try
            {
                if (!ModelState.IsValid) return View(paquete); //verifica que el modelo de vista sea valido

                var original = services.buscarPaquete(paquete.Id); //buscamos el paquete por el id 
                if (original == null) return NotFound(); //tira error si esta vacio

                //Detectar si cambió algo que afecta al tracking ya sea tienda de origen o fecha de registro
                bool cambiaPrefijo = !string.Equals(original.TiendaOrigen, paquete.TiendaOrigen, StringComparison.Ordinal);
                bool cambiaFecha = original.FechaRegistro != paquete.FechaRegistro;

                //Se permite actualizar los campos
                original.Peso = paquete.Peso;
                original.ValorTotalBruto = paquete.ValorTotalBruto;
                original.TiendaOrigen = paquete.TiendaOrigen;
                original.CondicionEspecial = paquete.CondicionEspecial;
                original.FechaRegistro = paquete.FechaRegistro;
                original.ClienteAsociado = paquete.ClienteAsociado;

                // Si cambió tienda o fecha → regenerar tracking y garantizar unicidad
                if (cambiaPrefijo || cambiaFecha)
                {
                    const int maxIntentos = 10;
                    bool ok = false;

                    for (int i = 0; i < maxIntentos; i++)
                    {
                        original.GenerarTracking(); // usa TiendaOrigen + FechaRegistro nuevos

                        //Si el tracking recién generado ya existe y NO es el del propio paquete, reintenta
                        if (!services.ExisteTracking(original.NumeroTracking) ||
                            string.Equals(original.NumeroTracking, paquete.NumeroTracking, StringComparison.Ordinal))
                        {
                            ok = true;
                            break;
                        }
                    }

                    //en caso de que la variable ok sea true, genera mensaje de error
                    if (!ok)
                    {
                        ModelState.AddModelError(nameof(Paquete.NumeroTracking),"No fue posible generar un número de tracking único. Intente nuevamente.");
                        return View(paquete);
                    }
                }
                // Si NO cambió tienda ni fecha → dejamos el tracking como estaba

                services.actualizarPaquete(original);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al actualizar el paquete.");
                return View(paquete);
            }
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

        // GET: PaqueteController/ReportePorCliente?cedula=123456789
        public ActionResult ReportePorCliente(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
            {
                TempData["Mensaje"] = "Debe ingresar una cédula válida para generar el reporte.";
                return RedirectToAction(nameof(Index));
            }

            var lista = services.ReportePaquetesPorCliente(cedula).Cast<Paquete>().ToList();

            ViewBag.Cedula = cedula;
            ViewBag.Total = lista.Count;

            return View(lista); // Vista fuertemente tipada a IEnumerable<Paquete>
        }







    }
}
