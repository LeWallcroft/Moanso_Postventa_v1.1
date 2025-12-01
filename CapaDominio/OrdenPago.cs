using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class OrdenPago
    {
        public int OrdenPagoID { get; set; }
        public int OrdentrabajoID { get; set; }
        public string Serie { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public decimal Total { get; set; }
    }
}
