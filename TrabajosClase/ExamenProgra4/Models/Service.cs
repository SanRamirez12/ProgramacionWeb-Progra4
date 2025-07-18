using System.Data.Entity;

namespace ExamenProgra4.Models
{
    public class Service: DbContext
    {
        public DbSet<Chofer> choferes { get; set; }

        public Service() : base("UberCR") { }

        public void agregarChofer(Chofer chofer)
        {

            choferes.Add(chofer); //Agrega direcctamente a la DB
            SaveChanges(); // Y guarda los cambios de la DB osea el commit
        }

        public Array mostrarChoferes()
        {
            return choferes.ToArray(); //Devuelve la lista de los estudiantes
        }

        public Chofer buscarChofer(int id)
        {
            var choferBuscado = this.choferes.FirstOrDefault(x => x.Id == id);
            if (choferBuscado != null)
                return choferBuscado;
            else throw new Exception("Este chofer no esta registrado");
        }

        public void eliminarChofer(Chofer chofer)
        {
            this.choferes.Remove(chofer);
            SaveChanges();
        }

        public void actualizarChofer(Chofer chofer)
        {
            var choferAnterior = this.choferes.FirstOrDefault(x => x.Id == chofer.Id);
            if (choferAnterior != null)
            {
                choferAnterior.CedulaIdentidad = chofer.CedulaIdentidad;
                choferAnterior.Nombre= chofer.Nombre;
                choferAnterior.FechaRegistro = chofer.FechaRegistro;
                choferAnterior.Calificacion =  chofer.Calificacion;
                choferAnterior.Categoria = chofer.Categoria;
                choferAnterior.ComisionGenerada = chofer.ComisionGenerada;
                choferAnterior.NoMascotas = chofer.NoMascotas;
                choferAnterior.NoViajesNocturnos = chofer.NoViajesNocturnos;
                choferAnterior.NoMaletasGrandes = chofer.NoMaletasGrandes;
                choferAnterior.NoNinos = chofer.NoNinos;
                SaveChanges();
            }
            else throw new Exception("Este chofer no esta registrado");
        }








    }
}
