using Aeropost.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aeropost.Controllers
{
    public class ClienteController : Controller
    {
        //Instanciamos service
        private Service services;
        public ClienteController()
        {
            this.services = new Service();
        }


        // GET: ClienteController
        public ActionResult Index(string cedula)
        {
            var clientes = services.listarClientes(cedula);
            ViewBag.CedulaFiltro = cedula;
            return View(clientes);
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                { // Validaciones de unicidad usando TU Service
                    if (services.existeCedula(cliente.Cedula))
                        ModelState.AddModelError(nameof(Cliente.Cedula), "Ya existe un cliente con esta cédula.");

                    if (services.existeCorreo(cliente.Correo))
                        ModelState.AddModelError(nameof(Cliente.Correo), "Ya existe un cliente con este correo.");

                    if (!ModelState.IsValid)
                        return View(cliente);

                    services.agregarCliente(cliente);
                    return RedirectToAction("Index");
                }
            }
            catch
            {

            }
            return View();
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            var clienteAnterior = services.buscarCliente(id);
            return View(clienteAnterior);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Evitar duplicados al actualizar (idIgnorar = cliente.Id)
                    if (services.existeCedula(cliente.Cedula, cliente.Id))
                        ModelState.AddModelError(nameof(Cliente.Cedula), "Otra ficha ya usa esta cédula.");

                    if (services.existeCorreo(cliente.Correo, cliente.Id))
                        ModelState.AddModelError(nameof(Cliente.Correo), "Otra ficha ya usa este correo.");

                    if (!ModelState.IsValid)
                        return View(cliente);

                    services.actualizarCliente(cliente);
                    return RedirectToAction("Index");
                }

            }
            catch
            {

            }
            return View();
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {

            try
            {
                var clienteEliminado = services.buscarCliente(id);
                return View(clienteEliminado); 
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var cliente = services.buscarCliente(id);
                services.eliminarCliente(cliente);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Cliente/ReportePorTipo
        // Muestra la vista con el formulario vacío o con todos
        public ActionResult ReportePorTipo()
        {
            var lista = services.listarClientesPorTipo(""); // todos
            ViewBag.TipoSeleccionado = "Todos";
            ViewBag.Total = services.mostrarClientes().Length;

            return View(lista);
        }

        // POST: Cliente/ReportePorTipo
        // Recibe el tipo seleccionado en el formulario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportePorTipo(string tipo)
        {
            var lista = services.listarClientesPorTipo(tipo);
            ViewBag.TipoSeleccionado = string.IsNullOrWhiteSpace(tipo) ? "Todos" : tipo;
            ViewBag.Total = lista.Count;

            return View(lista);
        }

        [HttpGet]
        public IActionResult ClientePorCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula))
                return Json(null);

            var c = services.buscarClientePorCedula(cedula);
            if (c == null) return Json(null);

            return Json(new
            {
                id = c.Id,
                nombre = c.Nombre,
                cedula = c.Cedula,
                tipo = c.Tipo,
                correo = c.Correo,
                direccion = c.Direccion,
                telefono = c.Telefono
            });
        }
    }
}
