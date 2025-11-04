using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class CapacidadDia
    {
        public int CapacidadId { get; set; }
        public string Bahia { get; set; } // Nombre de la bahía
        public TimeSpan HoraInicio { get; set; } // Mapea TIME en SQL
        public TimeSpan HoraFin { get; set; } // Mapea TIME en SQL
        public int CapacidadMax { get; set; }
        public int CapacidadReservada { get; set; }
        public int CuposLibres { get; set; } // Campo calculado en el SP

        // Propiedad para mostrar la hora en el DataGrid de forma amigable
        public string RangoHorario => $"{HoraInicio:hh\\:mm} - {HoraFin:hh\\:mm}";
    }
}
