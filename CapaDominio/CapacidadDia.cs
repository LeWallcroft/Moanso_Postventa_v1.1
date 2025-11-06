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
        public DateTime Fecha { get; set; }
        public string Bahia { get; set; }
        public int BahiaId { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int CapacidadMax { get; set; }
        public int CapacidadReservada { get; set; }
        public int CuposLibres { get; set; }
        public string Tipo { get; set; }

        public string RangoHorario => $"{HoraInicio:hh\\:mm} - {HoraFin:hh\\:mm}";
    }
}