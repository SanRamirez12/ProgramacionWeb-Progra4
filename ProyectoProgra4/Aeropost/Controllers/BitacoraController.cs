using System;
using System.Linq;
using Aeropost.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aeropost.Controllers
{
    public class BitacoraController : Controller
    {
        private readonly Service services;

        public BitacoraController()
        {
            this.services = new Service();
        }

        //Solo un metodo en el controller para ver los ingresos de usuarios

        // GET: Bitacora
        public IActionResult Index(string usuario = null, DateTime? desde = null, DateTime? hasta = null)
        {
            var datos = services.listarBitacora(usuario, desde, hasta).ToList();
            ViewBag.UsuarioFiltro = usuario;
            ViewBag.Desde = desde?.ToString("yyyy-MM-dd");
            ViewBag.Hasta = hasta?.ToString("yyyy-MM-dd");
            return View(datos);
        }

        //liberar recursos del controlador cuando se este usando antes de que el objeto del controlador sea destruido por el framework.
        protected override void Dispose(bool disposing)
        {
            if (disposing) services.Dispose();
            base.Dispose(disposing);
        }
    }
}
