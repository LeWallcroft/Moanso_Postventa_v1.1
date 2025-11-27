using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDominio
{
    public class Repuesto
    {
        public int RepuestoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public string Codigo { get; set; }
        public string Proveedor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }

        // Propiedades de validación
        public bool StockBajo => Stock <= StockMinimo;
        public bool TieneStock => Stock > 0;
    }
}
