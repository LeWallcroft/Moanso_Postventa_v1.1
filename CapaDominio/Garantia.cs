using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Garantia
    {
        public int GarantiaID { get; set; } 
        public int VehiculoID { get; set; } 
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public string TipoGarantia { get; set; } 
        public string Descripcion { get; set; }  

        public int? KilometrajeCobertura { get; set; } 

        public bool Activo { get; set; } 
        public DateTime FechaCreacion { get; set; }
    }
}
