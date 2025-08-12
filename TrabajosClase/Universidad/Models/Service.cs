using System.Data.Entity;

namespace Universidad.Models
{
    public class Service: DbContext
    {
        //Enlace de la aplicacion a la base de datos 
        public DbSet<Estudiante> estudiantes { get; set; } 
        public DbSet<Carrera> carreras { get; set; }
        public DbSet<Usuario>  usuarios { get; set; }

        //Nombre de la base de datos tiene que ser igual que el archivo de configuracion 
        public Service(): base("Universidad") { }


        #region Metodos de Estudiante

        //Metodos Estudiantes
        public void agregarEstudiante(Estudiante estudiante) {
            
            estudiantes.Add(estudiante); //Agrega direcctamente a la DB
            SaveChanges(); // Y guarda los cambios de la DB osea el commit
        }

        public Array mostrarEstudiantes() { 
            return estudiantes.ToArray(); //Devuelve la lista de los estudiantes
        }

        public Estudiante buscarEstudiante(int id) { 
            var estudianteBuscado = this.estudiantes.FirstOrDefault(x  => x.Id == id);
            if (estudianteBuscado != null)
                return estudianteBuscado;
            else throw new Exception("Ese estudiante no esta registrado");
        }

        public void eliminarEstudiante(Estudiante estudiante)
        {
            this.estudiantes.Remove(estudiante);
            SaveChanges();
        }

        public void actualizarEstudiante(Estudiante estudiante)
        {
            var estudianteAnterior = this.estudiantes.FirstOrDefault(x => x.Id == estudiante.Id);
            if (estudianteAnterior != null)
            {
                estudianteAnterior.Nombre = estudiante.Nombre;
                estudianteAnterior.Carrera = estudiante.Carrera;
                estudianteAnterior.CantidadCursos = estudiante.CantidadCursos;
                SaveChanges();
            }
            else throw new Exception("Ese estudiante no esta registrado");
        }

        #endregion

        #region Metodos de carreras
        //Metodos de carrreras
        public void agregarCarrera(Carrera carrera) {
            carreras.Add(carrera);
            SaveChanges();
        }

        public Array mostrarCarreras()
        {
            return carreras.ToArray();
        }

        public Carrera buscarCarrera(int id)
        {
            var carreraBuscada = this.carreras.FirstOrDefault(x => x.Id == id);
            if (carreraBuscada != null)
                return carreraBuscada;
            else throw new Exception("Esa carrera no esta registrada");
        }

        public void eliminarCarrera(Carrera carrera)
        {
            this.carreras.Remove(carrera);
            SaveChanges();
        }

        public void actualizarCarrera(Carrera carrera)
        {
            var carreraAnterior = this.carreras.FirstOrDefault(x => x.Id == carrera.Id);
            if (carreraAnterior != null)
            {
                carreraAnterior.Facultad = carrera.Facultad;
                carreraAnterior.NombreCarrera = carrera.NombreCarrera;
                carreraAnterior.Certificacion = carrera.Certificacion;
                carreraAnterior.Sede = carrera.Sede;
                SaveChanges();
            }
            else throw new Exception("Esa carrera no esta registrada");
        }

        #endregion

        #region Metodos de Usuarios
        //Metodos Usuarios
        public void agregarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
            SaveChanges();
        }

        public Array mostrarUsuarios()
        {
            return usuarios.ToArray();
        }

        public Usuario buscarUsuario(int id)
        {
            var usuarioBuscado = this.usuarios.FirstOrDefault(x => x.Id == id);
            if (usuarioBuscado != null)
                return usuarioBuscado;
            else throw new Exception("Ese usuario no esta registrada");
        }

        public void eliminarUsuario(Usuario usuario)
        {
            this.usuarios.Remove(usuario);
            SaveChanges();
        }

        public void actualizarUsuario(Usuario usuario)
        {
            var usuarioAnterior = this.usuarios.FirstOrDefault(x => x.Id == usuario.Id);
            if (usuarioAnterior != null)
            {
                usuarioAnterior.Nombre = usuario.Nombre;
                usuarioAnterior.Perfil = usuario.Perfil;
                usuarioAnterior.Fecha_vencimiento = usuario.Fecha_vencimiento;
                usuarioAnterior.Telefono = usuario.Telefono;
                usuarioAnterior.Email = usuario.Email;
                usuarioAnterior.Username = usuario.Username;
                usuarioAnterior.Password = usuario.Password;
                
                SaveChanges();
            }
            else throw new Exception("Ese usuario no esta registrado");
        }

        public Usuario login(string username, string password) { 
        
            var usuarioLogueado = usuarios.FirstOrDefault(u=> u.Username == username && u.Password == password);
            if (usuarioLogueado != null)
                return usuarioLogueado;
            else throw new Exception("Datos de inicio de sesión incorrectos o el usuario no existe. Por favor digite de nuevo");


        }
        



        #endregion


    }
}
