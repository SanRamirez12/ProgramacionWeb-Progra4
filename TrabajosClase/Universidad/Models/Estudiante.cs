using System.ComponentModel.DataAnnotations;

namespace Universidad.Models
{
    public class Estudiante
    {
        private int id;
        private string nombre;
        private string carrera;
        private int cantidadCursos;
        private string username;

        public Estudiante(int id, string nombre, string carrera, int cantidadCursos, string username)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Carrera = carrera;
            this.CantidadCursos = cantidadCursos;
            this.Username = username;
        }

        public Estudiante()
        {
            this.Id = 0;
            this.Nombre = "";
            this.Carrera = "";
            this.CantidadCursos = 0;
            this.Username = "";
        }

        [Key]
        public int Id { get => id; set => id = value; }


        public string Nombre { get => nombre; set => nombre = value; }
        public string Carrera { get => carrera; set => carrera = value; }
        public int CantidadCursos { get => cantidadCursos; set => cantidadCursos = value; }
        public string Username { get => username; set => username = value; }
    }
}
