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
            return View();
        }

        // POST: FacturaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Factura factura)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.agregarFactura(factura);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                // Log / manejar error si es necesario
            }
            return View(factura);
        }

        // GET: FacturaController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var facturaAnterior = services.buscarFactura(id);
                return View(facturaAnterior);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: FacturaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Factura factura)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.actualizarFactura(factura);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                // Log / manejar error si es necesario
            }
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
    }

   // GET: /Factura/BuscarPorCedula?cedula=1-2345-0678
[HttpGet]
        [Produces("application/json")]
        public IActionResult BuscarPorCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                return BadRequest(new { mensaje = "Debe indicar una cédula." });

            // LLAMA A LA ÚNICA FIRMA (sin ambigüedades)
            var datos = services.ObtenerDatosFacturaPorCedula(cedula);

            if (datos == null)
                return NotFound(new { mensaje = "No se encontraron datos para esa cédula." });

            return Ok(new
            {
                numeroTracking = datos.NumeroTracking,
                cedulaCliente = datos.CedulaCliente,
                peso = datos.Peso,
                valorTotalPaquete = datos.ValorTotalPaquete,
                esProductoEspecial = datos.EsProductoEspecial,
                fechaEntrega = datos.FechaEntrega.ToString("yyyy-MM-dd"),
                montoTotal = datos.MontoTotal
            });
        }

    }
