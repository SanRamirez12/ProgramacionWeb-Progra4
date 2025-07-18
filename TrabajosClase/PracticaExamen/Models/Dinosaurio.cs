using System.ComponentModel.DataAnnotations;

namespace PracticaExamen.Models
{
    public class Dinosaurio
    {
        private int idDinosaurio;
        private string nombreDinosaurio;
        private bool genero;
        private string alimentacion;
        private string fechaRegistro;
        private string tipoDinosaurio;
        private int edad;
        private string consideracionesImportantes;

        public Dinosaurio(int idDinosaurio, string nombreDinosaurio, bool genero, string alimentacion, string fechaRegistro, string tipoDinosaurio, int edad, string consideracionesImportantes)
        {
            this.IdDinosaurio = idDinosaurio;
            this.NombreDinosaurio = nombreDinosaurio;
            this.Genero = genero;
            this.Alimentacion = alimentacion;
            this.FechaRegistro = fechaRegistro;
            this.TipoDinosaurio = tipoDinosaurio;
            this.Edad = edad;
            this.ConsideracionesImportantes = consideracionesImportantes;
        }

        public Dinosaurio()
        {
            this.IdDinosaurio = 0;
            this.NombreDinosaurio = "";
            this.Genero = true;
            this.Alimentacion = "";
            this.FechaRegistro = "";
            this.TipoDinosaurio = "";
            this.Edad = 0;
            this.ConsideracionesImportantes = "";
        }

        [Key][Required]
        public int IdDinosaurio { get => idDinosaurio; set => idDinosaurio = value; }
        public string NombreDinosaurio { get => nombreDinosaurio; set => nombreDinosaurio = value; }
        public bool Genero { get => genero; set => genero = value; }
        public string Alimentacion { get => alimentacion; set => alimentacion = value; }
        public string FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public string TipoDinosaurio { get => tipoDinosaurio; set => tipoDinosaurio = value; }
        public int Edad { get => edad; set => edad = value; }
        public string ConsideracionesImportantes { get => consideracionesImportantes; set => consideracionesImportantes = value; }


    }
}
