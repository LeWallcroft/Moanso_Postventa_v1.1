using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class BahiaHorarioDTO
    {
        public int BahiaID { get; set; }
        public string NombreBahia { get; set; }
        public int CapacidadTotal { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int CuposDisponibles { get; set; }
        public string HorarioTexto => $"{HoraInicio:hh\\:mm} - {HoraFin:hh\\:mm}";
    }
}
