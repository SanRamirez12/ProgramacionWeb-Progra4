using System.ComponentModel.DataAnnotations;

namespace Universidad.Models
{
    public class Usuario
    {
        private int id;
        private string nombre;
        private string perfil;
        private DateTime fecha_vencimiento;
        private string telefono;
        private string email;
        private string username;
        private string password;


        public Usuario(int id, string nombre, string perfil, DateTime fecha_vencimiento, string telefono, string email, string username, string password)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Perfil = perfil;
            this.Fecha_vencimiento = fecha_vencimiento;
            this.Telefono = telefono;
            this.Email = email;
            this.Username = username;
            this.Password = password;
        }

        public Usuario()
        {
            this.Id = 0;
            this.Nombre = "";
            this.Perfil = "";
            this.Fecha_vencimiento = DateTime.Now;
            this.Telefono = "";
            this.Email = "";
            this.Username = "";
            this.Password = "";
        }

        [Key][Required]
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Perfil { get => perfil; set => perfil = value; }
        public DateTime Fecha_vencimiento { get => fecha_vencimiento; set => fecha_vencimiento = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Email { get => email; set => email = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }


    }
}
