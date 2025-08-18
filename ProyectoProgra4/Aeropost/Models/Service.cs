using System.Data.Entity;

namespace Aeropost.Models
{
    public class Service: DbContext
    {
        //Creacion de mapeos de tablas a base de datos:
        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Paquete> paquetes { get; set; }

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
        #endregion

        #region Metodos de Paquete
        public void agregarPaquetes(Paquete paquete)
        {

            paquetes.Add(paquete); //Agrega direcctamente a la DB
            SaveChanges(); // Y guarda los cambios de la DB osea el commit
        }

        public Array mostrarPaquetes()
        {
            return paquetes.ToArray(); //Devuelve la lista de los paquetes
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
        #endregion

        #region Metodos de Factura

        #endregion

        #region Metodos de Factura

        #endregion

        #region Metodos de Usuarios

        #endregion

        #region Metodos de Bitacora

        #endregion

    }
}
