using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El {0} no puede exceder {1} caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [StringLength(20, ErrorMessage = "La {0} no puede exceder {1} caracteres.")]
        // Si deseas solo números, descomenta:
        // [RegularExpression(@"^\d{1,20}$", ErrorMessage = "La {0} debe contener solo números.")]
        public string Cedula { get; set; } = string.Empty;

        [Display(Name = "Género")]
        [Required(ErrorMessage = "Debe seleccionar el {0}.")]
        [StringLength(10, ErrorMessage = "El {0} no puede exceder {1} caracteres.")]
        public string Genero { get; set; } = string.Empty;

        [Display(Name = "Fecha de Registro")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La {0} no tiene un formato válido (dd/mm/aaaa).")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Debe seleccionar el {0}.")]
        [StringLength(20, ErrorMessage = "El {0} no puede exceder {1} caracteres.")]
        public string Estado { get; set; } = string.Empty;

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El {0} no tiene un formato válido.")]
        [StringLength(100, ErrorMessage = "El {0} no puede exceder {1} caracteres.")]
        public string Correo { get; set; } = string.Empty;

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [StringLength(50, ErrorMessage = "El {0} no puede exceder {1} caracteres.")]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La {0} debe tener entre {2} y {1} caracteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        // Solo para formularios (no BD / no validado por EF)
        [NotMapped]
        [ValidateNever]
        [Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
