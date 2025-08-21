using System.ComponentModel.DataAnnotations;

namespace Aeropost.Models
{
    public class Paquete
    {
        private int id;
        private decimal peso;
        private decimal valorTotalBruto;
        private string tiendaOrigen;
        private bool condicionEspecial;
        private DateTime fechaRegistro;
        private string clienteAsociado;
        private string numeroTracking;

        public Paquete(int id, decimal peso, decimal valorTotalBruto, string tiendaOrigen, bool condicionEspecial, DateTime fechaRegistro, string clienteAsociado, string numeroTracking)
        {
            this.id = id;
            this.peso = peso;
            this.valorTotalBruto = valorTotalBruto;
            this.tiendaOrigen = tiendaOrigen;
            this.condicionEspecial = condicionEspecial;
            this.fechaRegistro = fechaRegistro;
            this.clienteAsociado = clienteAsociado;
            this.NumeroTracking = ""; // se generará después
        }

        public Paquete()
        {
            this.Id = 0;
            this.Peso = 0;
            this.ValorTotalBruto = 0;
            this.TiendaOrigen = "";
            this.CondicionEspecial = true;
            this.FechaRegistro = DateTime.Now;
            this.ClienteAsociado = "";
            this.NumeroTracking = ""; // se generará después
        }


        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Peso (lb)")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [Range(0.01, 999999.99, ErrorMessage = "El {0} debe estar entre {1} y {2}.")]
        public decimal Peso { get; set; }

        [Display(Name = "Valor Total Bruto ($)")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [Range(0.01, 999999999.99, ErrorMessage = "El {0} debe estar entre {1} y {2}.")]
        [DataType(DataType.Currency, ErrorMessage = "El {0} no tiene un formato válido.")]
        public decimal ValorTotalBruto { get; set; }

        [Display(Name = "Tienda de Origen")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "La {0} debe tener entre {2} y {1} caracteres.")]
        public string TiendaOrigen { get; set; }

        [Display(Name = "Condición Especial")]
        [Required(ErrorMessage = "Debe indicar la {0}.")]
        public bool CondicionEspecial { get; set; }

        [Display(Name = "Fecha de Registro")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La {0} no tiene un formato válido (dd/mm/aaaa).")]
        public DateTime FechaRegistro { get; set; }

        [Display(Name = "Cédula del Cliente")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [StringLength(100, ErrorMessage = "La {0} no puede exceder {1} caracteres.")]
        [RegularExpression(@"^\d{1,20}$", ErrorMessage = "La {0} solo debe contener números.")] //solo números
        public string ClienteAsociado { get; set; }

        [Display(Name = "Número de Tracking")]
        [StringLength(32, ErrorMessage = "El {0} no puede exceder {1} caracteres.")]
        public string NumeroTracking { get => numeroTracking; set => numeroTracking = value; }

        //Metodos de Logica de Paquetes:

        // Random estático para reducir colisiones si se llama seguido.
        private static readonly Random _rng = new();

        // Método para crear el número de tracking (sin validaciones adicionales)
        public void GenerarTracking()
        {
            var pref = TiendaOrigen.Substring(0, 2).ToUpper();
            var mes = FechaRegistro.ToString("MM");
            var yy = FechaRegistro.ToString("yy");
            var rand = _rng.Next(0, 100000).ToString("D5");

            NumeroTracking = $"{pref}{mes}{yy}MIA{rand}";
        }
        
    }
}
