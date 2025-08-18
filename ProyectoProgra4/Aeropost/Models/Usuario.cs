using System.ComponentModel.DataAnnotations;

namespace Aeropost.Models
{
    public class Usuario
    {
        // Campos privados
        private int id;
        private string nombreUsuario;
        private string contrasena;
        private string correo;
        private string rol;

        // Constructores
        public Usuario(int id, string nombreUsuario, string contrasena, string correo, string rol)
        {
            this.Id = id;
            this.NombreUsuario = nombreUsuario;
            this.Contrasena = contrasena;
            this.Correo = correo;
            this.Rol = rol;
        }

        public Usuario()
        {
            this.Id = 0;
            this.NombreUsuario = "";
            this.Contrasena = "";
            this.Correo = "";
            this.Rol = "";
        }

        // Propiedades
        [Key]
        [Required]
        public int Id { get => id; set => id = value; }

        [Required]
        [StringLength(30)]
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Contrasena { get => contrasena; set => contrasena = value; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Correo { get => correo; set => correo = value; }

        [Required]
        [StringLength(20)]
        public string Rol { get => rol; set => rol = value; }
    }
}
