using System.Data.Entity;

namespace PracticaExamen.Models
{
    public class Service : DbContext{

        public DbSet<Dinosaurio> dinosaurios { get; set; }

        public Service() : base("PracticaExamen") { }

        public void agregarDinosaurio(Dinosaurio dinosaurio){
            dinosaurios.Add(dinosaurio);
            SaveChanges();
        }   

        public Array mostrarDinosarios(){
            return dinosaurios.ToArray();
        }

        public Dinosaurio buscarDinosaurio(int id){
            var dinosaurioBuscado = this.dinosaurios.FirstOrDefault(x => x.IdDinosaurio == id);
            if (dinosaurioBuscado != null)
            {
                return dinosaurioBuscado;
            }
            else throw new Exception("Ese dinosaurio no esta registrado");
            
        }

        public void eliminarDinosaurio(Dinosaurio dinosaurio){
            
            this.dinosaurios.Remove(dinosaurio);
            SaveChanges();
          
        }

        public void actualizarDinosaurio(Dinosaurio dinosaurio)
        {
            var dinosaurioAnterior = this.dinosaurios.FirstOrDefault(x => x.IdDinosaurio == dinosaurio.IdDinosaurio);
            if (dinosaurioAnterior != null)
            {
                dinosaurioAnterior.NombreDinosaurio = dinosaurio.NombreDinosaurio;
                dinosaurioAnterior.Genero = dinosaurio.Genero;
                dinosaurioAnterior.Alimentacion = dinosaurio.Alimentacion;
                dinosaurioAnterior.FechaRegistro = dinosaurio.FechaRegistro;
                dinosaurioAnterior.TipoDinosaurio = dinosaurio.TipoDinosaurio;
                dinosaurioAnterior.Edad = dinosaurio.Edad;
                dinosaurioAnterior.ConsideracionesImportantes = dinosaurio.ConsideracionesImportantes;
                SaveChanges();
            }
            else
            {
                throw new Exception("Ese dinosaurio no esta registrado");
            }
        }





















    }
}
