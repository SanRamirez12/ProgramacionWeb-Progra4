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
        [Required]
        public int Id { get; set; }   

        [Required]
        [Range(0, 999999.99)]
        [DataType(DataType.Currency)]
        public decimal Peso { get; set; }   

        [Required]
        [Range(0, 999999999.99)]
        [DataType(DataType.Currency)]
        public decimal ValorTotalBruto { get; set; }   

        [Required]
        [StringLength(100)]
        public string TiendaOrigen { get; set; }   

        [Required]
        public bool CondicionEspecial { get; set; }   

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaRegistro { get; set; }   

        [Required]
        [StringLength(100)]
        public string ClienteAsociado { get; set; }
        
        [StringLength(32)]
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
