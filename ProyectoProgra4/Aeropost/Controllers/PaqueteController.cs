using Aeropost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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
                // Validación: la cédula debe existir como cliente
                var ced = (paquete.ClienteAsociado ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(ced))
                {
                    ModelState.AddModelError(nameof(Paquete.ClienteAsociado), "La cédula es obligatoria.");
                    return View(paquete);
                }
                var cliente = services.buscarClientePorCedula(ced);
                if (cliente == null)
                {
                    ModelState.AddModelError(nameof(Paquete.ClienteAsociado), "La cédula no pertenece a ningún cliente registrado.");
                    return View(paquete);
                }
                // normaliza el valor
                paquete.ClienteAsociado = ced;

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
                                                               // 🔒 Validación: la cédula debe existir como cliente
                var ced = (paquete.ClienteAsociado ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(ced))
                {
                    ModelState.AddModelError(nameof(Paquete.ClienteAsociado), "La cédula es obligatoria.");
                    return View(paquete);
                }
                var cliente = services.buscarClientePorCedula(ced);
                if (cliente == null)
                {
                    ModelState.AddModelError(nameof(Paquete.ClienteAsociado), "La cédula no pertenece a ningún cliente registrado.");
                    return View(paquete);
                }
                // normaliza el valor
                paquete.ClienteAsociado = ced;

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
                var paquete = services.buscarPaquete(id);

                // Para mostrar nombre en la vista (si el cliente existe)
                var cli = services.buscarClientePorCedula(paquete.ClienteAsociado);
                ViewBag.ClienteNombre = cli?.Nombre;

                return View(paquete);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
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

        // GET: Paquete/ReportePorCliente  (sin cédula => muestra form)
        // GET: Paquete/ReportePorCliente?cedula=123456789  (con cédula => lista)
        public ActionResult ReportePorCliente(string? cedula)
        {
            // 1) Sin cédula -> solo formulario
            if (string.IsNullOrWhiteSpace(cedula))
            {
                ViewBag.Cedula = null;
                ViewBag.ClienteNombre = null;
                ViewBag.TotalPaquetes = 0;
                ViewBag.PesoTotal = 0m;
                ViewBag.ValorBrutoTotal = 0m;
                ViewBag.ConEspecial = 0;

                return View(Enumerable.Empty<Paquete>());
            }

            // 2) Con cédula -> validar cliente y consultar
            cedula = cedula.Trim();
            var cliente = services.buscarClientePorCedula(cedula);
            if (cliente == null)
            {
                ModelState.AddModelError(string.Empty, "La cédula no pertenece a ningún cliente registrado.");
                ViewBag.Cedula = cedula;
                ViewBag.ClienteNombre = null;
                ViewBag.TotalPaquetes = 0;
                ViewBag.PesoTotal = 0m;
                ViewBag.ValorBrutoTotal = 0m;
                ViewBag.ConEspecial = 0;

                return View(Enumerable.Empty<Paquete>());
            }

            var lista = services.ReportePaquetesPorCliente(cedula);

            // 3) Totales en el controller (sin out)
            var total = lista.Count;
            var pesoTotal = lista.Sum(p => p.Peso);
            var valorBrutoTotal = lista.Sum(p => p.ValorTotalBruto);
            var conEspecial = lista.Count(p => p.CondicionEspecial);

            ViewBag.Cedula = cedula;
            ViewBag.ClienteNombre = cliente.Nombre;
            ViewBag.TotalPaquetes = total;
            ViewBag.PesoTotal = pesoTotal;
            ViewBag.ValorBrutoTotal = valorBrutoTotal;
            ViewBag.ConEspecial = conEspecial;

            return View(lista);
        }

        // GET: PaqueteController/PaquetePorTracking?tracking=ABC123
        public IActionResult PaquetePorTracking(string tracking)
        {
            if (string.IsNullOrWhiteSpace(tracking))
                return Json(null);

            var p = services.buscarPaquetePorTracking(tracking);
            if (p == null) return Json(null);

            var c = services.buscarClientePorCedula(p.ClienteAsociado);

            return Json(new
            {
                paquete = new
                {
                    tracking = p.NumeroTracking,
                    peso = p.Peso,
                    valor = p.ValorTotalBruto,
                    especial = p.CondicionEspecial,
                    cedulaCliente = p.ClienteAsociado
                },
                cliente = (c == null ? null : new
                {
                    id = c.Id,
                    nombre = c.Nombre,
                    cedula = c.Cedula,
                    tipo = c.Tipo,
                    correo = c.Correo,
                    direccion = c.Direccion,
                    telefono = c.Telefono
                })
            });
        }







    }
}
