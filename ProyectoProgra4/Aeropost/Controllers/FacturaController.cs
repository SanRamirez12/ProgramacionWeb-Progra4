using Aeropost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aeropost.Controllers
{
    public class FacturaController : Controller
    {
        // Instanciamos service
        private Service services;
        public FacturaController()
        {
            this.services = new Service();
        }

        // GET: FacturaController
        public ActionResult Index()
        {
            var facturas = services.mostrarFacturas();
            return View(facturas);
        }

        // GET: FacturaController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var factura = services.buscarFactura(id);
                return View(factura);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: FacturaController/Create
        public ActionResult Create()
        {
            ViewBag.Clientes = services.mostrarClientes()
                                       .Cast<Cliente>()
                                       .OrderBy(c => c.Nombre)
                                       .ToList();

            return View(new Factura { FechaEntrega = DateTime.Now });
        }

        // POST: FacturaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Factura factura)
        {
            try
            {
                // Los rellena/calcula el Service, así que quitamos su validación del POST
                ModelState.Remove(nameof(Factura.Peso));
                ModelState.Remove(nameof(Factura.ValorTotalPaquete));
                ModelState.Remove(nameof(Factura.EsProductoEspecial));
                ModelState.Remove(nameof(Factura.MontoTotal));

                if (ModelState.IsValid)
                {
                    services.agregarFactura(factura); // valida/rellena/calcula
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            // Si hay error, recargamos lista de clientes para que el select no quede vacío
            ViewBag.Clientes = services.mostrarClientes().Cast<Cliente>().OrderBy(c => c.Nombre).ToList();
            return View(factura);
        }

        // GET: FacturaController/Edit/5
        public ActionResult Edit(int id)
        {
            var factura = services.buscarFactura(id);

            ViewBag.Clientes = services.mostrarClientes()
                                       .Cast<Cliente>()
                                       .OrderBy(c => c.Nombre)
                                       .ToList();

            return View(factura);
        }

        // POST: FacturaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Factura factura)
        {
            try
            {
                // Igual que en Create: los rellena el Service
                ModelState.Remove(nameof(Factura.Peso));
                ModelState.Remove(nameof(Factura.ValorTotalPaquete));
                ModelState.Remove(nameof(Factura.EsProductoEspecial));
                ModelState.Remove(nameof(Factura.MontoTotal));

                if (ModelState.IsValid)
                {
                    services.actualizarFactura(factura);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            // 🔸 Importante: si falló, volver a cargar clientes para que el select no quede vacío
            ViewBag.Clientes = services.mostrarClientes()
                                       .Cast<Cliente>()
                                       .OrderBy(c => c.Nombre)
                                       .ToList();
            return View(factura);
        }

        // GET: FacturaController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var facturaEliminada = services.buscarFactura(id);
                return View(facturaEliminada);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: FacturaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var factura = services.buscarFactura(id);
                services.eliminarFactura(factura);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public IActionResult PaquetesPorCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                return Json(new object[0]);

            var paquetes = services
                .ReportePaquetesPorCliente(cedula)          // ya lo tienes
                .Cast<Paquete>()
                .OrderByDescending(p => p.FechaRegistro)
                .Select(p => new {
                    tracking = p.NumeroTracking,
                    peso = p.Peso,
                    valor = p.ValorTotalBruto,
                    especial = p.CondicionEspecial
                })
                .ToList();

            return Json(paquetes);
        }

        // GET: Factura/ReporteMensual?anio=2025&mes=8
        public ActionResult ReporteMensual(int? anio, int? mes)
        {
            if (anio == null || mes == null)
            {
                ViewBag.Anio = DateTime.Now.Year;
                ViewBag.Mes = DateTime.Now.Month;
                ViewBag.Total = 0m;
                ViewBag.Cantidad = 0;
                return View(new List<Factura>());
            }

            var lista = services.listarFacturasPorMes(anio.Value, mes.Value);
            var resumen = services.totalFacturadoDelMes(anio.Value, mes.Value);

            ViewBag.Anio = anio.Value;
            ViewBag.Mes = mes.Value;
            ViewBag.Total = resumen.total;
            ViewBag.Cantidad = resumen.cantidad;

            return View(lista);
        }

        // GET: Factura/PorCedula?cedula=123456789
        public ActionResult PorCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                return View(new List<Factura>());

            var lista = services.listarFacturasPorCedula(cedula);
            ViewBag.Cedula = cedula;
            ViewBag.Total = lista.Sum(f => f.MontoTotal);
            ViewBag.Cantidad = lista.Count;

            return View(lista);
        }

        // GET: Factura/PorTracking?tracking=AB0825MIA12345
        public ActionResult PorTracking(string tracking)
        {
            if (string.IsNullOrWhiteSpace(tracking))
                return View(model: null);

            var f = services.buscarFacturaPorTracking(tracking);
            return View(f);
        }

    }

}
