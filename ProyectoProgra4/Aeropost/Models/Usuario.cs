using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aeropost.Models
{
    public class Usuario
    {
        private int id;
        private string nombre;
        private string cedula;
        private string genero;
        private DateTime fechaRegistro;
        private string estado;
        private string correo;
        private string username;
        private string password;


        public Usuario(int id, string nombre, string cedula, string genero, DateTime fechaRegistro, string estado, string correo, string username, string password)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Cedula = cedula;
            this.Genero = genero;
            this.FechaRegistro = fechaRegistro;
            this.Estado = estado;
            this.Correo = correo;
            this.Username = username;
            this.Password = password;
        }

        public Usuario()
        {
            this.Id = 0;
            this.Nombre = "";
            this.Cedula = "";
            this.Genero = "";
            this.FechaRegistro = DateTime.Now;
            this.Estado = "";
            this.Correo = "";
            this.Username = "";
            this.Password = "";
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get => id; set => id = value; }

        [Required]
        [StringLength(100)]
        public string Nombre { get => nombre; set => nombre = value; }

        [Required]
        [StringLength(20)]
        public string Cedula { get => cedula; set => cedula = value; }

        [Required]
        [StringLength(10)]
        public string Genero { get => genero; set => genero = value; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }

        [Required]
        [StringLength(20)]
        public string Estado { get => estado; set => estado = value; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Correo { get => correo; set => correo = value; }

        [Required]
        [StringLength(50)]
        public string Username { get => username; set => username = value; }

        [Required, StringLength(100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        //Propiedad auxiliar de solo validacion
        [NotMapped]//no se guarda en BD
        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")] //compara dos propiedades
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
