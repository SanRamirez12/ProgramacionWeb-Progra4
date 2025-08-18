using System.ComponentModel.DataAnnotations;

namespace Aeropost.Models
{
    public class Cliente
    {
        private int id;
        private string nombre;
        private string cedula;
        private string tipo;
        private string correo;
        private string direccion;
        private string telefono;


        public Cliente(int id, string nombre, string cedula, string tipo, string correo, string direccion, string telefono)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Cedula = cedula;
            this.Tipo = tipo;
            this.Correo = correo;
            this.Direccion = direccion;
            this.Telefono = telefono;
        }

        public Cliente()
        {
            this.Id = 0;
            this.Nombre = "";
            this.Cedula = "";
            this.Tipo = "";
            this.Correo = "";
            this.Direccion = "";
            this.Telefono = "";
        }

        [Key][Required]
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Cedula { get => cedula; set => cedula = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }



    }
}
