using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Cita_2
    {
        public int CitaId { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraInicio { get; set; } // Tomada del slot de CapacidadDia
        public TimeSpan HoraFin { get; set; } // Tomada del slot de CapacidadDia
        public string Cliente { get; set; }
        public string Vehiculo { get; set; }
        public string Servicio { get; set; }
        public string Tecnico { get; set; } // UsuarioId del técnico
        public string Bahia { get; set; } // Tomada del slot de CapacidadDia
        public string Estado { get; set; } // Ej: Reservada, En Proceso, Terminada

    }
}
