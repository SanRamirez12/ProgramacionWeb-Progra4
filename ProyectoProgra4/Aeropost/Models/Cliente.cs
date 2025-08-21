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
        [Display(Name = "Id")]
        public int Id { get => id; set => id = value; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El {0} no puede exceder {1} caracteres.")]
        public string Nombre { get => nombre; set => nombre = value; }

        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [StringLength(20, ErrorMessage = "La {0} no puede exceder {1} caracteres.")]
        [RegularExpression(@"^\d{1,20}$", ErrorMessage = "La {0} solo debe contener números.")]//cédula es solo números
        public string Cedula { get => cedula; set => cedula = value; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [StringLength(50, ErrorMessage = "El {0} no puede exceder {1} caracteres.")]
        public string Tipo { get => tipo; set => tipo = value; }

        [Display(Name = "Correo")]
        [Required(ErrorMessage = "El {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El {0} no tiene un formato válido.")]
        [StringLength(150, ErrorMessage = "El {0} no puede exceder {1} caracteres.")]
        public string Correo { get => correo; set => correo = value; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [StringLength(250, ErrorMessage = "La {0} no puede exceder {1} caracteres.")]
        [DataType(DataType.MultilineText)]
        public string Direccion { get => direccion; set => direccion = value; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "La {0} es obligatoria.")]
        [StringLength(20, ErrorMessage = "El {0} no puede exceder {1} caracteres.")]
        [Phone(ErrorMessage = "El {0} no tiene un formato válido.")]
        [RegularExpression(@"^\d{8,20}$", ErrorMessage = "El {0} debe tener solo números (8 a 20 dígitos).")]// solo números (sin guiones/espacios)
        public string Telefono { get => telefono; set => telefono = value; }


    }
}
