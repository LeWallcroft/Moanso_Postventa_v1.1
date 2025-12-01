using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class RepuestoOT
    {
        public int OtrepuestoID { get; set; }
        public int OrdentrabajoID { get; set; }
        public int RepuestoID { get; set; }

        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        /// <summary>
        /// true = viene de Repuestosservicio (por defecto, no se puede eliminar)
        /// false = agregado manualmente en esta pantalla
        /// </summary>
        public bool EsDefault { get; set; }
    }
}
