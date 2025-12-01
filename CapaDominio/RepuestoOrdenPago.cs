using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class RepuestoOrdenPago
    {

        public int OtrepuestoID { get; set; }
        public int OrdentrabajoID { get; set; }
        public int RepuestoID { get; set; }

        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        // true = viene de RepuestosServicio (no se puede eliminar)
        public bool EsDefault { get; set; }
    }
}
