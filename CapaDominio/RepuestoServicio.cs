using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class RepuestoServicio
    {
        public int RepuestosservicioID { get; set; }
        public int RepuestoID { get; set; }
        public int ServicioID { get; set; }
        public int Cantidad { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Propiedades de navegación
        public string RepuestoNombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int StockActual { get; set; }
    }
}
