using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Cita
    {
        public int CitaId { get; set; }
        public DateTime FechaCita { get; set; }
        public TimeSpan HoraInicio { get; set; } // Tomada del slot de CapacidadDia
        public int ClienteId { get; set; }
        public int VehiculoId { get; set; }
        public int ServicioId { get; set; }
        public int TecnicoId { get; set; } // UsuarioId del técnico
        public int BahiaId { get; set; } // Tomada del slot de CapacidadDia
        public string Estado { get; set; } // Ej: Reservada, En Proceso, Terminada
        public int CapacidadId { get; set; } // Referencia al slot de CalendarioCapacidad
        public DateTime FechaCreacion { get; set; }
    }
}
