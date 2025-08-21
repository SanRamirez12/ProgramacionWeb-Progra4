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
        public Cliente buscarClientePorCedula(string cedula)
        {
            return this.clientes.FirstOrDefault(c => c.Cedula == cedula);
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
        public Paquete buscarPaquetePorTracking(string tracking)
        {
            return this.paquetes.FirstOrDefault(p => p.NumeroTracking == tracking);
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
            // 1) Unicidad por tracking
            if (existeFacturaPorTracking(factura.NumeroTracking))
                throw new Exception("Ya existe una factura para ese número de tracking.");

            // 2) Traer el paquete
            var paquete = paquetes.FirstOrDefault(p => p.NumeroTracking == factura.NumeroTracking);
            if (paquete == null)
                throw new Exception("No existe un paquete con ese número de tracking.");

            // 3) Coherencia de cédula
            if (paquete.ClienteAsociado != factura.CedulaCliente)
                throw new Exception("La cédula no coincide con el cliente del paquete.");

            // 4) Rellenar desde Paquete y calcular
            factura.Peso = paquete.Peso;
            factura.ValorTotalPaquete = paquete.ValorTotalBruto;
            factura.EsProductoEspecial = paquete.CondicionEspecial;
            factura.MontoTotal = calcularMontoFactura(paquete);

            // 5) Fecha por defecto
            if (factura.FechaEntrega == default) factura.FechaEntrega = DateTime.Now;

            facturas.Add(factura);
            SaveChanges();
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
            var anterior = this.facturas.FirstOrDefault(x => x.Id == factura.Id);
            if (anterior == null) throw new Exception("Esa factura no está registrada");

            bool cambiaTracking = !string.Equals(anterior.NumeroTracking, factura.NumeroTracking, StringComparison.Ordinal);

            // Si cambia tracking, validar duplicado y existencia del paquete
            if (cambiaTracking && existeFacturaPorTracking(factura.NumeroTracking))
                throw new Exception("Ya existe otra factura con ese número de tracking.");

            var paquete = paquetes.FirstOrDefault(p => p.NumeroTracking == factura.NumeroTracking);
            if (paquete == null)
                throw new Exception("No existe un paquete con ese número de tracking.");

            // Coherencia de cédula
            if (paquete.ClienteAsociado != factura.CedulaCliente)
                throw new Exception("La cédula no coincide con el cliente del paquete.");

            // Actualizar campos editables
            anterior.NumeroTracking = factura.NumeroTracking;
            anterior.CedulaCliente = factura.CedulaCliente;
            anterior.FechaEntrega = factura.FechaEntrega == default ? anterior.FechaEntrega : factura.FechaEntrega;

            // Rellenar/calcular siempre desde el paquete para garantizar consistencia
            anterior.Peso = paquete.Peso;
            anterior.ValorTotalPaquete = paquete.ValorTotalBruto;
            anterior.EsProductoEspecial = paquete.CondicionEspecial;
            anterior.MontoTotal = calcularMontoFactura(paquete);

            SaveChanges();
        }

        // ¿Existe factura para este tracking? (evitar duplicados)
        public bool existeFacturaPorTracking(string tracking)
        {
            return facturas.Any(f => f.NumeroTracking == tracking);
        }

        // Buscar factura por tracking exacto
        public Factura buscarFacturaPorTracking(string tracking)
        {
            return facturas.FirstOrDefault(f => f.NumeroTracking == tracking);
        }

        // Listar facturas por cédula del cliente (más recientes primero)
        public List<Factura> listarFacturasPorCedula(string cedula)
        {
            return facturas
                .Where(f => f.CedulaCliente == cedula)
                .OrderByDescending(f => f.FechaEntrega)
                .ToList();
        }

        // Listar facturas de un mes específico
        public List<Factura> listarFacturasPorMes(int anio, int mes)
        {
            return facturas
                .Where(f => f.FechaEntrega.Year == anio && f.FechaEntrega.Month == mes)
                .OrderByDescending(f => f.FechaEntrega)
                .ToList();
        }

        // Total facturado del mes (monto total y cantidad)
        public (decimal total, int cantidad) totalFacturadoDelMes(int anio, int mes)
        {
            var lista = listarFacturasPorMes(anio, mes);
            decimal total = lista.Sum(f => f.MontoTotal);
            return (total, lista.Count);
        }

        // --- Cálculo del monto (privado de apoyo) ---
        private decimal calcularMontoFactura(Paquete p)
        {
            decimal tarifaBase = 12m;                     // 12 USD por kg
            decimal basePeso = p.Peso * tarifaBase;
            decimal iva = p.ValorTotalBruto * 0.13m;
            decimal adicional = p.CondicionEspecial ? (p.ValorTotalBruto * 0.10m) : 0m;
            return Math.Round(basePeso + iva + adicional, 2);
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
                usuarioAnterior.Nombre = usuario.Nombre;
                usuarioAnterior.Genero = usuario.Genero;
                usuarioAnterior.FechaRegistro = usuario.FechaRegistro;
                usuarioAnterior.Estado = usuario.Estado;
                usuarioAnterior.Correo = usuario.Correo;
                usuarioAnterior.Username = usuario.Username;
                usuarioAnterior.Password = usuario.Password;

                SaveChanges();
            }
            else
                throw new Exception("Ese usuario no está registrado");
        }

        //Metodo que setea un usario admin generico para hacer el login inicial
        public Usuario login(string username, string password)
        {
            // Caso especial: usuario de emergencia
            if (username == "adminKing" && password == "1234567")
            {
                return new Usuario
                {
                    Id = -1,
                    Nombre = "Administrador Genérico",
                    Cedula = "000000000",
                    Genero = "N/A",
                    FechaRegistro = DateTime.Now,
                    Estado = "Activo",
                    Correo = "admin@aeropost.com",
                    Username = "adminKing",
                    Password = "1234567"
                };
            }

            // Buscar usuario en la lista normal
            var usuario = usuarios.FirstOrDefault(u =>
                u.Username == username && u.Password == password);

            if (usuario == null)
                throw new Exception("Usuario o contraseña incorrectos.");

            return usuario;
        }


        //Valida que dos contraseñas coincidan (comparación ordinal).
        public bool PasswordsCoinciden(string password, string confirmPassword)
        {
            return string.Equals(password, confirmPassword, StringComparison.Ordinal);
        }

        #endregion

        #region Metodos de Bitacora

        #endregion

    }
}
