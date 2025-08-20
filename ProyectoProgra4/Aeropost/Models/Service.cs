using System.Data.Entity;

namespace Aeropost.Models
{
    public class Service: DbContext
    {
        //Creacion de mapeos de tablas a base de datos:
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Paquete> paquetes { get; set; }
        public DbSet<Factura> facturas { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        // Hola
        //Creacion del context con la base Aeropost:
        public Service(): base("Aeropost") { }

        //Metodos del CRUD por clase logica resumidos en regions:

        #region Metodos de Cliente
        public void agregarCliente(Cliente cliente)
        {

            clientes.Add(cliente); //Agrega direcctamente a la DB
            SaveChanges(); // Y guarda los cambios de la DB osea el commit
        }

        public Array mostrarClientes()
        {
            return clientes.ToArray(); //Devuelve la lista de los clientes
        }

        public Cliente buscarCliente(int id)
        {
            var clienteBuscado = this.clientes.FirstOrDefault(x => x.Id == id);
            if (clienteBuscado != null)
                return clienteBuscado;
            else throw new Exception("Ese cliente no esta registrado");
        }

        public void eliminarCliente(Cliente cliente)
        {
            this.clientes.Remove(cliente);
            SaveChanges();
        }

        public void actualizarCliente(Cliente cliente)
        {
            var clienteAnterior = this.clientes.FirstOrDefault(x => x.Id == cliente.Id);
            if (clienteAnterior != null){
               
                clienteAnterior.Nombre = cliente.Nombre;
                clienteAnterior.Cedula = cliente.Cedula;
                clienteAnterior.Correo = cliente.Correo;
                clienteAnterior.Telefono = cliente.Telefono;
                clienteAnterior.Tipo = cliente.Tipo;
                clienteAnterior.Direccion = cliente.Direccion;
                SaveChanges();
            }
            else throw new Exception("Ese cliente no esta registrado");
        }


        //  Listar (con filtro opcional por cédula para el Index)
        public List<Cliente> listarClientes(string filtroCedula = null)
        {
            if (string.IsNullOrWhiteSpace(filtroCedula))
                return this.clientes.OrderBy(c => c.Nombre).ToList();

            return this.clientes
                .Where(c => c.Cedula.Contains(filtroCedula))
                .OrderBy(c => c.Nombre)
                .ToList();
        }

        //  Reporte por tipo (Regular / Frecuente / Suspendido)
        public List<Cliente> listarClientesPorTipo(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                return this.clientes.OrderBy(c => c.Nombre).ToList();

            return this.clientes
                .Where(c => c.Tipo == tipo)
                .OrderBy(c => c.Nombre)
                .ToList();
        }

        //  Unicidad sencilla (para Create/Edit)
        //    Nota: usa 'idIgnorar' cuando estás editando (para no chocarte contigo mismo).
        public bool existeCedula(string cedula, int idIgnorar = 0)
        {
            return this.clientes.Any(c => c.Cedula == cedula && c.Id != idIgnorar);
        }

        public bool existeCorreo(string correo, int idIgnorar = 0)
        {
            return this.clientes.Any(c => c.Correo == correo && c.Id != idIgnorar);
        }
        #endregion

        #region Metodos de Paquete
        public void agregarPaquete(Paquete paquete)
        {
            paquetes.Add(paquete);
            SaveChanges();
        }

        public Array mostrarPaquete()
        {
            return paquetes.ToArray();
        }

        public Paquete buscarPaquete(int id)
        {
            var paqueteBuscado = this.paquetes.FirstOrDefault(x => x.Id == id);
            if (paqueteBuscado != null)
                return paqueteBuscado;
            else throw new Exception("Ese paquete no esta registrado");
        }

        public void eliminarPaquete(Paquete paquete)
        {
            this.paquetes.Remove(paquete);
            SaveChanges();
        }

        public void actualizarPaquete(Paquete paquete)
        {
            var paqueteAnterior = this.paquetes.FirstOrDefault(x => x.Id == paquete.Id);
            if (paqueteAnterior != null)
            {
                paqueteAnterior.Peso = paquete.Peso;
                paqueteAnterior.TiendaOrigen = paquete.TiendaOrigen;
                paqueteAnterior.CondicionEspecial = paquete.CondicionEspecial;
                paqueteAnterior.ValorTotalBruto = paquete.ValorTotalBruto;
                paqueteAnterior.FechaRegistro = paquete.FechaRegistro;
                paqueteAnterior.ClienteAsociado = paquete.ClienteAsociado;
                SaveChanges();
            }
            else throw new Exception("Ese paquete no esta registrado");
        }

        //Metodo para validar si ya existe el numero de tracking
        public bool ExisteTracking(string numeroTracking)
        {
            return paquetes.Any(p => p.NumeroTracking == numeroTracking);
        }

        //Devuelve TODOS los paquetes de un cliente (por cédula), ordenados del más reciente al más antiguo.
        public Array ReportePaquetesPorCliente(string cedulaCliente)
        {
            return paquetes
                .Where(p => p.ClienteAsociado == cedulaCliente)
                .OrderByDescending(p => p.FechaRegistro)
                .ToArray();
        }

        #endregion


        #region Metodos de Factura
        public void agregarFactura(Factura factura)
        {
            facturas.Add(factura); // Agrega directamente a la DB
            SaveChanges(); // Guarda los cambios en la DB (commit)
        }

        public Array mostrarFacturas()
        {
            return facturas.ToArray(); // Devuelve la lista de facturas
        }

        public Factura buscarFactura(int id)
        {
            var facturaBuscada = this.facturas.FirstOrDefault(x => x.Id == id);
            if (facturaBuscada != null)
                return facturaBuscada;
            else throw new Exception("Esa factura no está registrada");
        }

        public void eliminarFactura(Factura factura)
        {
            this.facturas.Remove(factura);
            SaveChanges();
        }

        public void actualizarFactura(Factura factura)
        {
            var facturaAnterior = this.facturas.FirstOrDefault(x => x.Id == factura.Id);
            if (facturaAnterior != null)
            {
                facturaAnterior.NumeroTracking = factura.NumeroTracking;
                facturaAnterior.CedulaCliente = factura.CedulaCliente;
                facturaAnterior.Peso = factura.Peso;
                facturaAnterior.ValorTotalPaquete = factura.ValorTotalPaquete;
                facturaAnterior.EsProductoEspecial = factura.EsProductoEspecial;
                facturaAnterior.FechaEntrega = factura.FechaEntrega;
                facturaAnterior.MontoTotal = factura.MontoTotal;

                SaveChanges();
            }
            else throw new Exception("Esa factura no está registrada");
        }

        #endregion

        #region Metodos de Usuarios
        public void agregarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario); // Agrega directamente a la DB
            SaveChanges(); // Guarda los cambios en la DB (commit)
        }

        public Array mostrarUsuarios()
        {
            return usuarios.ToArray(); // Devuelve la lista de los usuarios
        }

        public Usuario buscarUsuario(int id)
        {
            var usuarioBuscado = this.usuarios.FirstOrDefault(x => x.Id == id);
            if (usuarioBuscado != null)
                return usuarioBuscado;
            else
                throw new Exception("Ese usuario no está registrado");
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
                usuarioAnterior.NombreUsuario = usuario.NombreUsuario;
                usuarioAnterior.Contrasena = usuario.Contrasena;
                usuarioAnterior.Correo = usuario.Correo;
                usuarioAnterior.Rol = usuario.Rol;

                SaveChanges();
            }
            else
                throw new Exception("Ese usuario no está registrado");
        }
        public Usuario login(string username, string password)
        {
            var usuarioLogueado = usuarios.FirstOrDefault(u => u.NombreUsuario == username && u.Contrasena == password);
            if (usuarioLogueado != null)
                return usuarioLogueado;
            else throw new Exception("Usuario o contraseña es incorrecto");
        }

        #endregion

        #region Metodos de Bitacora

        #endregion

    }
}
