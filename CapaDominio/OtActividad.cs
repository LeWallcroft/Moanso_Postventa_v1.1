using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class OtActividad
    {
        public int OtactividadID { get; set; }
        public int OrdentrabajoID { get; set; }
        public int? ActividadTiposervicioID { get; set; }

        public string Descripcion { get; set; }
        public string Estado { get; set; }

        public int? TiempoEstimado { get; set; }
        public int? TiempoReal { get; set; }

        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public string Observaciones { get; set; }
    }
}
