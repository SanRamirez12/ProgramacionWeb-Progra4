using System.ComponentModel.DataAnnotations;

namespace ExamenProgra4.Models
{
    public class Chofer
    {
        int id;
        string cedulaIdentidad;
        string nombre;
        DateTime fechaRegistro;
        int calificacion;
        string categoria;
        decimal comisionGenerada;
        bool noMascotas;
        bool noNinos;
        bool noMaletasGrandes;
        bool noViajesNocturnos;


        public Chofer(int id, string cedulaIdentidad, string nombre, DateTime fechaRegistro, int calificacion, string categoria, decimal comisionGenerada, bool noMascotas, bool noNinos, bool noMaletasGrandes, bool noViajesNocturnos)
        {
            this.Id = id;
            this.CedulaIdentidad = cedulaIdentidad;
            this.Nombre = nombre;
            this.FechaRegistro = fechaRegistro;
            this.Calificacion = calificacion;
            this.Categoria = categoria;
            this.ComisionGenerada = comisionGenerada;
            this.NoMascotas = noMascotas;
            this.NoNinos = noNinos;
            this.NoMaletasGrandes = noMaletasGrandes;
            this.NoViajesNocturnos = noViajesNocturnos;
        }

        public Chofer()
        {
            this.Id = 0;
            this.CedulaIdentidad = "";
            this.Nombre = "";
            this.FechaRegistro = DateTime.Now;
            this.Calificacion = 0;
            this.Categoria = "";
            this.ComisionGenerada = 0;
            this.NoMascotas = true;
            this.NoNinos = true;
            this.NoMaletasGrandes = true;
            this.NoViajesNocturnos = true;
        }

        [Key]
        [Required]
        public int Id { get => id; set => id = value; }
        public string CedulaIdentidad { get => cedulaIdentidad; set => cedulaIdentidad = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public DateTime FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public int Calificacion { get => calificacion; set => calificacion = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public decimal ComisionGenerada { get => comisionGenerada; set => comisionGenerada = value; }
        public bool NoMascotas { get => noMascotas; set => noMascotas = value; }
        public bool NoNinos { get => noNinos; set => noNinos = value; }
        public bool NoMaletasGrandes { get => noMaletasGrandes; set => noMaletasGrandes = value; }
        public bool NoViajesNocturnos { get => noViajesNocturnos; set => noViajesNocturnos = value; }


        public static decimal CalcularBonificacion(string categoria, int calificacion, DateTime fechaRegistro, decimal comisionGenerada)
        {
            //Se calcula el factor de calificación
            decimal factorCalificacion = calificacion switch
            {
                >= 4 => 0.3m,
                3 => 0.2m,
                _ => 0.1m
            };

            //Se calculan los años de servicio
            int años = DateTime.Now.Year - fechaRegistro.Year;
            if (DateTime.Now.Month < fechaRegistro.Month ||
               (DateTime.Now.Month == fechaRegistro.Month && DateTime.Now.Day < fechaRegistro.Day))
            {
                años--;
            }

            //Se calcula el bonus por categoría
            decimal bonusCategoria = categoria switch
            {
                "UberX" => 0m,
                "UberXL" => 2000m,
                "Comfort" => 3000m,
                "UberBlack" => 5000m,
                _ => 0m
            };

            //Se calcula la bonificación final
            decimal bonificacion = (comisionGenerada * factorCalificacion) + (años * 500m) + bonusCategoria;

            return bonificacion;
        }









    }
}
