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

        public Paquete(int id, decimal peso, decimal valorTotalBruto, string tiendaOrigen, bool condicionEspecial, DateTime fechaRegistro, string clienteAsociado)
        {
            this.Id = id;
            this.Peso = peso;
            this.ValorTotalBruto = valorTotalBruto;
            this.TiendaOrigen = tiendaOrigen;
            this.CondicionEspecial = condicionEspecial;
            this.FechaRegistro = fechaRegistro;
            this.ClienteAsociado = clienteAsociado;
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

    }
}
