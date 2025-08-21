using System;
using System.ComponentModel.DataAnnotations;

namespace Aeropost.Models
{
    public class Factura
    {
        // Campos privados
        private int id;
        private string numeroTracking;
        private string cedulaCliente;
        private decimal peso;
        private decimal valorTotalPaquete;
        private bool esProductoEspecial;
        private DateTime fechaEntrega;
        private decimal montoTotal;

        // Constructores
        public Factura(int id, string numeroTracking, string cedulaCliente,
                       decimal peso, decimal valorTotalPaquete,
                       bool esProductoEspecial, DateTime fechaEntrega, decimal montoTotal)
        {
            this.Id = id;
            this.NumeroTracking = numeroTracking;
            this.CedulaCliente = cedulaCliente;
            this.Peso = peso;
            this.ValorTotalPaquete = valorTotalPaquete;
            this.EsProductoEspecial = esProductoEspecial;
            this.FechaEntrega = fechaEntrega;
            this.MontoTotal = montoTotal;
        }

        public Factura()
        {
            this.Id = 0;
            this.NumeroTracking = "";
            this.CedulaCliente = "";
            this.Peso = 0m;
            this.ValorTotalPaquete = 0m;
            this.EsProductoEspecial = false;
            this.FechaEntrega = DateTime.Now;
            this.MontoTotal = 0m;
        }

        // Propiedades
        [Key]
        [Required(ErrorMessage = "El Id es obligatorio.")]
        public int Id { get => id; set => id = value; }

        [Required(ErrorMessage = "El Número de Tracking es obligatorio.")]
        [StringLength(30, ErrorMessage = "El Número de Tracking no puede superar los 30 caracteres.")]
        [Display(Name = "Número de Tracking")]
        public string NumeroTracking { get => numeroTracking; set => numeroTracking = value; }

        [Required(ErrorMessage = "La Cédula del Cliente es obligatoria.")]
        [StringLength(25, ErrorMessage = "La Cédula no puede superar los 25 caracteres.")]
        [Display(Name = "Cédula del Cliente")]
        public string CedulaCliente { get => cedulaCliente; set => cedulaCliente = value; }

        [Required(ErrorMessage = "El Peso es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El Peso debe ser mayor a 0.")]
        [Display(Name = "Peso (lb)")]
        public decimal Peso { get => peso; set => peso = value; }

        [Required(ErrorMessage = "El Valor del Paquete es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El Valor del Paquete no puede ser negativo.")]
        [Display(Name = "Valor del Paquete ($)")]
        public decimal ValorTotalPaquete { get => valorTotalPaquete; set => valorTotalPaquete = value; }

        [Required(ErrorMessage = "Debe indicar si el producto es especial.")]
        [Display(Name = "Producto Especial")]
        public bool EsProductoEspecial { get => esProductoEspecial; set => esProductoEspecial = value; }

        [Required(ErrorMessage = "La Fecha de Entrega es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "El formato de la fecha no es válido.")]
        [Display(Name = "Fecha de Entrega")]
        public DateTime FechaEntrega { get => fechaEntrega; set => fechaEntrega = value; }

        [Required(ErrorMessage = "El Monto Total es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El Monto Total no puede ser negativo.")]
        [Display(Name = "Monto Total ($)")]
        public decimal MontoTotal { get => montoTotal; set => montoTotal = value; }

    }
}
