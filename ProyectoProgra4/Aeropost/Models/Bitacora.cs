using System;
using System.ComponentModel.DataAnnotations;

namespace Aeropost.Models
{
    public class Bitacora
    {
        private int id;
        private string username;
        private DateTime fechaIngreso;
        private DateTime? fechaSalida;

        public Bitacora()
        {
            this.id = 0;
            this.username = string.Empty;
            this.fechaIngreso = DateTime.UtcNow;
            this.fechaSalida = null;
        }

        public Bitacora(int id, string username, DateTime fechaIngreso, DateTime? fechaSalida = null)
        {
            this.id = id;
            this.username = username;
            this.fechaIngreso = fechaIngreso;
            this.fechaSalida = fechaSalida;
        }

        [Key] public int Id { get { return id; } set { id = value; } }
        [Required, StringLength(256)] public string Username { get { return username; } set { username = value; } }
        [Required, Display(Name = "Fecha de ingreso")] public DateTime FechaIngreso { get { return fechaIngreso; } set { fechaIngreso = value; } }
        [Display(Name = "Fecha de salida")] public DateTime? FechaSalida { get { return fechaSalida; } set { fechaSalida = value; } }
    }
}

