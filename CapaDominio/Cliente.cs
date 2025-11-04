using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; } // Mapea a Telefono1
        public string Email { get; set; }
        public string DNI { get; set; } // Renombrado a DNI para consistencia con la interfaz
        public DateTime FechaRegistro { get; set; }
        public string NombreCompleto => $"{Nombres} {Apellidos}";
    }
}
