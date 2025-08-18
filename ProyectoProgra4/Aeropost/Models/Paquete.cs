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

        [Key][Required]
        public int Id { get => id; set => id = value; }
        public decimal Peso { get => peso; set => peso = value; }
        public decimal ValorTotalBruto { get => valorTotalBruto; set => valorTotalBruto = value; }
        public string TiendaOrigen { get => tiendaOrigen; set => tiendaOrigen = value; }
        public bool CondicionEspecial { get => condicionEspecial; set => condicionEspecial = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public string ClienteAsociado { get => clienteAsociado; set => clienteAsociado = value; }

    }
}
