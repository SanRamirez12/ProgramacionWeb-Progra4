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

        [Key]
        [Required]
        public int Id { get; set; }   

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }   

        [Required]
        [StringLength(20)]
        public string Cedula { get; set; }   

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }    

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Correo { get; set; }  

        [StringLength(250)]
        public string Direccion { get; set; }   

        [StringLength(20)]
        [Phone]
        public string Telefono { get; set; }    



    }
}
