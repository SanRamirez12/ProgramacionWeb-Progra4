using System.ComponentModel.DataAnnotations;

namespace Universidad.Models
{
    public class Estudiante
    {
        private int id;
        private string nombre;
        private string carrera;
        private int cantidadCursos;

        public Estudiante(int id, string nombre, string carrera, int cantidadCursos)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Carrera = carrera;
            this.CantidadCursos = cantidadCursos;
        }

        public Estudiante()
        {
            this.Id = 0;
            this.Nombre = "";
            this.Carrera = "";
            this.CantidadCursos = 0;
        }

        [Key]
        public int Id { get => id; set => id = value; }


        public string Nombre { get => nombre; set => nombre = value; }
        public string Carrera { get => carrera; set => carrera = value; }
        public int CantidadCursos { get => cantidadCursos; set => cantidadCursos = value; }

    }
}
