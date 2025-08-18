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
        public ActionResult Index()
        {
            var clientes = services.mostrarClientes();
            return View(clientes);
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                {
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


    }
}
